using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml;
using System.Data;

namespace EMS.Common
{
    public class CommonUtils
    {
        public CommonUtils()
        {

        }

        public string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }

        public DataTable ConvertExcelToDataTable(string excelFilePath)
        {
            DataTable dt = new DataTable();

            // Check if the file path is valid and the file exists
            if (!string.IsNullOrEmpty(excelFilePath) && File.Exists(excelFilePath))
            {
                // Set the EPPlus license context
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // Open the file as a stream
                using (FileStream stream = new FileStream(excelFilePath, FileMode.Open, FileAccess.Read))
                {
                    using (ExcelPackage package = new ExcelPackage())
                    {
                        package.Load(stream);

                        // Ensure there's at least one worksheet
                        if (package.Workbook.Worksheets.Count > 0)
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets.First();

                            int noOfCol = worksheet.Dimension.End.Column;
                            int noOfRow = worksheet.Dimension.End.Row;

                            // Read the column headers
                            for (int c = 1; c <= noOfCol; c++)
                            {
                                dt.Columns.Add(worksheet.Cells[1, c].Text);
                            }

                            // Read the data rows
                            for (int r = 2; r <= noOfRow; r++)
                            {
                                DataRow dr = dt.NewRow();
                                for (int c = 1; c <= noOfCol; c++)
                                {
                                    dr[c - 1] = worksheet.Cells[r, c].Value;
                                }
                                dt.Rows.Add(dr);
                            }
                        }
                    }
                }
            }

            return dt;
        }

    }
}

