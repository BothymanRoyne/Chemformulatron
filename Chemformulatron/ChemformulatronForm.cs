using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;
using System.IO;
using System.Text.RegularExpressions;

namespace Chemformulatron
{
    public partial class ChemformulatronForm : Form
    {
        private readonly string PeriodicTableCSVPath = Path.Combine(System.IO.Path.GetFullPath(@"..\..\"), "Resources", "periodictable.csv"); //adds the .csv file as a resource in the solution
        private readonly List<Element> Elements;
        private DataTable OriginalTable;

        public ChemformulatronForm()
        {
            InitializeComponent();

            Elements = new List<Element>();
            tboxChemicalFormula.Select(); //focus the chemical formula textbox on startup

            UpdateDGV(ProcessPeriodicTableCSV(PeriodicTableCSVPath, ",", true)); //builds a new DataTable from the CSV data with comma as delimiter and column header names enabled
            dgvElementInfo.ColumnHeaderMouseClick += DgvElementInfo_ColumnHeaderMouseClick;

            lblMolarMass.ForeColor = Color.Red; //initialize the molar mass output to be red (invalid)
        }

        /// <summary>
        /// Event handler for datagridview column click.
        /// Datagridview default column click is by string - override string sort to numeric sort
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvElementInfo_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //select which column was clicked
            switch (e.ColumnIndex)
            {
                //column 0 atomic number -  call numeric sort by atomic number
                case 0:
                    SortByAtomicNumber();
                    break;
                //column 3 atomic mass -  call numeric sort by atomic mass
                case 3:
                    SortByAtomicMass();
                    break;
                //column 4 melting point -  call numeric sort by melting point
                case 8:
                    SortByMeltingPoint();
                    break;
                //column 9 boiling point -  call numeric sort by boiling point
                case 9:
                    SortByBoilingPoint();
                    break;
            }
        }

        /// <summary>
        /// takes a CSV filename and delimiters as input and populates a datatable for output and a list representing the periodic table
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="delimiters"></param>
        /// <param name="firstRowContainsFieldNames"></param>
        private DataTable ProcessPeriodicTableCSV(string filename, string delimiters, bool firstRowContainsFieldNames = true)
        {
            DataTable periodicTable = new DataTable();

            using (TextFieldParser parser = new TextFieldParser(filename))
            {
                parser.TextFieldType = FieldType.Delimited; //field type is delimited
                parser.SetDelimiters(delimiters); //we'll pass in comma as our delimiter

                if (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields(); //read CSV data into string array

                    //add our columns from the first row in the CSV
                    for (int i = 0; i < fields.Length; i++)
                    {
                        if (firstRowContainsFieldNames)
                            periodicTable.Columns.Add(fields[i]);
                        else
                            periodicTable.Columns.Add("Col" + i);
                    }

                    //if there are no headers in the CSV, just add the field as a normal row
                    if (!firstRowContainsFieldNames)
                        periodicTable.Rows.Add(fields);
                }

                //add all the rows to the datatable
                while (!parser.EndOfData)
                {
                    string[] row = parser.ReadFields(); //read current row into string array
                    periodicTable.Rows.Add(row); //add the row to the table

                    //add the current row as an Element to our list
                    Element current = new Element(Convert.ToInt32(row[0]), row[1], row[2], row[3], row[4], row[5], row[6], row[7], row[8], row[9], row[10], row[11]);

                    Elements.Add(current);
                }
            }

            //save the original CSV as a DataTable, just in case
            OriginalTable = periodicTable;

            return periodicTable;
        }

        /// <summary>
        /// binds a DataTable as the DataSource for our DataGridView
        /// </summary>
        /// <param name="dataTable"></param>
        private void UpdateDGV(DataTable dataTable)
        {
            dgvElementInfo.DataSource = dataTable;
        }

        /// <summary>
        /// Updates molar mass calculation and quantity table when formula text box changes
        /// </summary>
        private void tboxChemicalFormula_TextChanged(object sender, EventArgs e)
        {
            // Regex filters matches into 5 groups, in order: Opening bracket, chemical symbol, quantity, closing bracket, group quantity
            Regex regex = new Regex(@"(\(?)([A-Z][a-z]?)(\d*)(\)?)(\d*)");

            // Searches the input textbox. If there were no matches, success is false so the table will not update and the molar mass text will be red
            MatchCollection matches = regex.Matches(tboxChemicalFormula.Text);
            bool success = regex.IsMatch(tboxChemicalFormula.Text);

            // Dictionaries for storing elements and their counts. The tempCounts is a temporary dictionary for storing elements found in brackets
            // use is for switching between the main and temporary dictionaries
            Dictionary<string, int> elementCounts = new Dictionary<string, int>();
            Dictionary<string, int> tempCounts = new Dictionary<string, int>();
            Dictionary<string, int> use = elementCounts;

            // Iterates through all regex matches looking for elements, their counts, and any groups
            foreach (Match match in matches)
            {
                // If the opening bracket group exists, then start recording all elements found to the temporary dictionary
                // success is set to false because if no closing bracket is found, the formula is not complete
                if (match.Groups[1].Value == "(")
                {
                    use = tempCounts;
                    use.Clear();
                    success = false;
                }

                // If an element is found in the element group, it is added to the dictionary in use with a count of 0
                if (!use.ContainsKey(match.Groups[2].Value))
                    use.Add(match.Groups[2].Value, 0);

                // The next group is the count for previously found elements. If there was an element, but no count in this group,
                // the last element has a count of 1 and is incremented in the active dictionary
                if (int.TryParse(match.Groups[3].Value, out int elementCount))
                    use[match.Groups[2].Value] += elementCount;
                else
                    use[match.Groups[2].Value]++;

                // Upon reaching a closing bracket, the active dictionary is checked. If it is not the temporary dictionary, then there was no opening bracket and
                // success is set to false
                if (match.Groups[4].Value == ")")
                {
                    if (use != tempCounts)
                        success = false;
                  
                    // The last group, containing the bracketed element multiplier, is checked. If it is not successfully parsed out, its quantity is 1
                    if (!int.TryParse(match.Groups[5].Value, out int multiplier))
                        multiplier = 1;

                    // Each element quantity in the temporary dictionary is multiplied by the multiplier, then values and (if necessary) keys are added to the main dictionary
                    // The main dictionary is then switched to be the active one
                    foreach (KeyValuePair<string, int> kvp in tempCounts)
                    {
                        if (!elementCounts.ContainsKey(kvp.Key))
                            elementCounts.Add(kvp.Key, 0);
                        elementCounts[kvp.Key] += kvp.Value * multiplier;
                    }
                    use = elementCounts;
                    success = true;
                }
            }

            // Creates an anonymous type of required information from the parsed elements. It is cross referenced with the list of elements so that 
            // only valid elements are used from here on
            var legitElements = from n in Elements
                                join m in elementCounts on n.Symbol equals m.Key
                                select new
                                {
                                    Element = n.Name,
                                    Count = m.Value,
                                    Mass = n.DoubleMass,
                                    TotalMass = n.DoubleMass * m.Value
                                };

            // If there are a different count of legit elements compared to the found ones, there are non-existant elements in the formula, so success is set false
            if (legitElements.Count() != elementCounts.Count())
                success = false;

            // Calculates the molar mass. If success is true, mass text is green to indicate as such. Text is red otherwise
            lblMolarMass.Text = $"{legitElements.Sum(i => i.TotalMass)} g/mol";
            if (!success)
            {
                lblMolarMass.ForeColor = Color.Red;
                return;
            }
            lblMolarMass.ForeColor = Color.Green;

            // If parsing was successful, the anonymous type of elements and their counts are displayed
            dgvElementInfo.DataSource = new BindingSource
            {
                DataSource = legitElements
            };
        }

        /// <summary>
        /// Event handler for 'sort by name' button click.
        /// Sort elements by element name and call update to datatable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSortByName_Click(object sender, EventArgs e)
        {
            //generate resultset of element properties ordered by name, as dynamic list
            var elementsByName = (from el in Elements
                                  orderby el.Name
                                  select new
                                  {
                                      el.Name,
                                      el.AtomicNumber,
                                      el.Symbol,
                                      el.Mass,
                                      el.CPKHexColor,
                                      el.Electronegativity,
                                      el.State,
                                      el.BondingType,
                                      el.MeltingPoint,
                                      el.BoilingPoint,
                                      el.GroupBlock,
                                      el.YearDiscovered
                                  }).ToList<dynamic>();

            //generate new data table (new binding source for datagridview) with source set elements sorted by name
            GenerateNewDataTable(elementsByName);
        }

        /// <summary>
        /// Event handler for 'sort by atomic number' button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSortByAtomicNum_Click(object sender, EventArgs e)
        {
            //call sort by atomic number method
            SortByAtomicNumber();
        }

        /// <summary>
        /// Sort elements by atomic mass and call update to datatable
        /// </summary>
        private void SortByAtomicMass()
        {
            //generate resultset of element properties ordered by atomic mass (float ordering), as dynamic list
            var elementsByAtomicMass = (from el in Elements
                                        orderby float.Parse(el.Mass)
                                        select new
                                        {
                                            el.Name,
                                            el.AtomicNumber,
                                            el.Symbol,
                                            el.Mass,
                                            el.CPKHexColor,
                                            el.Electronegativity,
                                            el.State,
                                            el.BondingType,
                                            el.MeltingPoint,
                                            el.BoilingPoint,
                                            el.GroupBlock,
                                            el.YearDiscovered
                                        }).ToList<dynamic>();

            //generate new data table (new binding source for datagridview) with source set elements sorted by atomic mass 
            GenerateNewDataTable(elementsByAtomicMass);
        }

        /// <summary>
        /// Sort elements by atomic number and call update to datatable
        /// </summary>
        private void SortByAtomicNumber()
        {
            //generate resultset of element properties ordered by atomic number (string ordering), as dynamic list
            var elementsByAtomicNum = (from el in Elements
                                       orderby el.AtomicNumber
                                       select new
                                       {
                                           el.Name,
                                           el.AtomicNumber,
                                           el.Symbol,
                                           el.Mass,
                                           el.CPKHexColor,
                                           el.Electronegativity,
                                           el.State,
                                           el.BondingType,
                                           el.MeltingPoint,
                                           el.BoilingPoint,
                                           el.GroupBlock,
                                           el.YearDiscovered
                                       }).ToList<dynamic>();

            //generate new data table (new binding source for datagridview) with source set elements sorted by atomic number 
            GenerateNewDataTable(elementsByAtomicNum);
        }

        /// <summary>
        /// Sort elements by boiling point and call update to datatable
        /// </summary>
        private void SortByBoilingPoint()
        {
            //generate resultset of element properties ordered by boiling point (float ordering), as dynamic list
            var elementsByAtomicNum = (from el in Elements
                                       orderby float.TryParse(el.BoilingPoint, out var value) ? value : float.MinValue //take melting point and parse as a float, if it is a float, output the value, else output the default minimum value of a float
                                       select new
                                       {
                                           el.Name,
                                           el.AtomicNumber,
                                           el.Symbol,
                                           el.Mass,
                                           el.CPKHexColor,
                                           el.Electronegativity,
                                           el.State,
                                           el.BondingType,
                                           el.MeltingPoint,
                                           el.BoilingPoint,
                                           el.GroupBlock,
                                           el.YearDiscovered
                                       }).ToList<dynamic>();

            //generate new data table (new binding source for datagridview) with source set elements sorted by boiling point
            GenerateNewDataTable(elementsByAtomicNum);
        }

        /// <summary>
        /// Sort elements by melting point and call update to datatable
        /// </summary>
        private void SortByMeltingPoint()
        {
            //generate resultset of element properties ordered by melting point (float ordering) , as dynamic list
            var elementsByMeltingPoint = (from el in Elements
                                          orderby float.TryParse(el.MeltingPoint, out var value) ? value : float.MinValue //take melting point and parse as a float, if it is a float, output the value, else output the default minimum value of a float
                                          select new
                                          {
                                              el.Name,
                                              el.AtomicNumber,
                                              el.Symbol,
                                              el.Mass,
                                              el.CPKHexColor,
                                              el.Electronegativity,
                                              el.State,
                                              el.BondingType,
                                              el.MeltingPoint,
                                              el.BoilingPoint,
                                              el.GroupBlock,
                                              el.YearDiscovered
                                          }).ToList<dynamic>();

            //generate new data table (new binding source for datagridview) with source set elements sorted by melting point
            GenerateNewDataTable(elementsByMeltingPoint);
        }

        /// <summary>
        /// Filter single character elements and sort by its alphabetical order, then update DGV with new datatable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSingleCharSymbols_Click(object sender, EventArgs e)
        {
            //generate resultset of element properties filtered by elements with symbol length = 1, and order by symbol (string ordering), as dynamic list
            var singleCharElements = (from el in Elements
                                      where el.Symbol.Length == 1
                                      orderby el.AtomicNumber
                                      select new
                                      {
                                          el.Name,
                                          el.AtomicNumber,
                                          el.Symbol,
                                          el.Mass,
                                          el.CPKHexColor,
                                          el.Electronegativity,
                                          el.State,
                                          el.BondingType,
                                          el.MeltingPoint,
                                          el.BoilingPoint,
                                          el.GroupBlock,
                                          el.YearDiscovered
                                      }).ToList<dynamic>();

            //generate new data table (new binding source for datagridview) with source set elements sorted by symbol
            GenerateNewDataTable(singleCharElements);
        }

        private void GenerateNewDataTable(List<dynamic> elements)
        {
            //datatable is used to display the element information in the datagridview
            DataTable dt = new DataTable();

            //add the column headers
            foreach (var v in OriginalTable.Columns)
                dt.Columns.Add(v.ToString());

            //add the row data
            foreach (var v in elements)
                dt.Rows.Add(v.AtomicNumber, v.Symbol, v.Name, v.Mass, v.CPKHexColor, v.Electronegativity, v.State, v.BondingType, v.MeltingPoint, v.BoilingPoint, v.GroupBlock, v.YearDiscovered);

            //binds the new datatable to the datagridview
            UpdateDGV(dt);
        }
    }

    //this class represents an element, contains element attribute properties
    public class Element
    {
        public string Name { get; set; }
        public int AtomicNumber { get; set; }
        public string Symbol { get; set; }
        public string Mass { get; set; }
        public string CPKHexColor { get; set; }
        public string Electronegativity { get; set; }
        public string State { get; set; }
        public string BondingType { get; set; }
        public string MeltingPoint { get; set; }
        public string BoilingPoint { get; set; }
        public string GroupBlock { get; set; }
        public string YearDiscovered { get; set; }

        //used when calculating the molar mass of a chemical formula
        public double DoubleMass
        {
            get
            {
                double.TryParse(Mass, out double mass);
                return mass;
            }
        }

        public Element(int atomicNum,
                       string symbol,
                       string name,
                       string mass,
                       string cpkHexColor,
                       string electronegativity,
                       string state,
                       string bondingType,
                       string meltingPt,
                       string boilingPt,
                       string groupBlock,
                       string yearDiscovered)
        {
            AtomicNumber = atomicNum;
            Symbol = symbol;
            Name = name;
            Mass = mass;
            CPKHexColor = cpkHexColor;
            Electronegativity = electronegativity;
            State = state;
            BondingType = bondingType;
            MeltingPoint = meltingPt;
            BoilingPoint = boilingPt;
            GroupBlock = groupBlock;
            YearDiscovered = yearDiscovered;
        }
    }
}
