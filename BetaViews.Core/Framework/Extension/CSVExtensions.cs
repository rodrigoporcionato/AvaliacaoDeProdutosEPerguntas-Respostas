using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace BetaViews.Core.Framework.Extension
{
    public static class CSVHelperExtension
    {


        public static void ExportToExcel<T>(this List<T> list, List<string> columnsName)
        {
            var report = new System.Data.DataTable("Report");
            columnsName.ForEach(x=> 
                report.Columns.Add(x, typeof(string))
            );            
            
            PropertyInfo[] propInfos = typeof(T).GetProperties();
            for (int i = 0; i <= list.Count -1; i++)
            {
                T item = list[i];
                var items = new List<string>();
                for (int j = 0; j <= propInfos.Length - 1; j++)
                {
                    object o = item.GetType().GetProperty(propInfos[j].Name).GetValue(item, null);
                    string value = "";

                    if (o == null)
                    {
                        value = "-";
                    }
                    else
                    {
                        value = o.ToString();
                    }

                    if (value.Contains("\r"))
                    {
                        value = value.Replace("\r", " ");
                    }
                    if (value.Contains("\n"))
                    {
                        value = value.Replace("\n", "+ & CHAR(10) & +");
                    }
                    items.Add(value);
                }
                report.Rows.Add(items.ToArray());
            }

            var grid = new GridView();
            grid.AllowPaging = false;
            grid.DataSource = report;            
            grid.DataBind();
            

            //grid.HeaderRow.Style.Add("background-color", "#FFFFFF");
            //grid.HeaderRow.Cells[0].Style.Add("background-color", "green");
            //grid.HeaderRow.Cells[1].Style.Add("background-color", "green");
            //grid.HeaderRow.Cells[2].Style.Add("background-color", "green");
            //grid.HeaderRow.Cells[3].Style.Add("background-color", "green");  

            //possivel hack no servidor para funcionar: http://www.itexperience.net/2008/03/17/excel-2007-error-different-format-than-specified-by-the-file-extension/
            /*
                Click Start, click Run, type regedit.exe and press ENTER. This will open your Registry
                Navigate to HKEY_CURRENT_USER\SOFTWARE\MICROSOFT\OFFICE\12.0\EXCEL\SECURITY
                Right click in the right window and choose New -> DWORD
                In the Name field, type “ExtensionHardening”  (without the quotes)
                Verify that the data has the value “0”
             */

            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.Buffer = true;                        
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=Report.xls");            
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grid.RenderControl(htw);


            HttpContext.Current.Response.Output.Write(sw.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }



        public static MemoryStream ExportToExcelV2<T>(this List<T> list, List<string> columnsName)
        {
            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet("TABELA_SUPRIMENTOS_SOC");
            var headerRow = sheet.CreateRow(0);

            //define o fundo da cor do header.
            ICellStyle style6 = workbook.CreateCellStyle();
            style6.FillForegroundColor = IndexedColors.SeaGreen.Index;
            style6.FillPattern = FillPattern.SolidForeground;
            style6.FillBackgroundColor = IndexedColors.White.Index;
            //define a cor da fonte do header
            IFont font2 = workbook.CreateFont();
            font2.Color = IndexedColors.White.Index;
            //font2.FontHeightInPoints = 15;            
            style6.SetFont(font2);

            int indexColumns = 0;
            columnsName.ToList().ForEach(x =>
            {
                headerRow.CreateCell(indexColumns).SetCellValue(x);
                headerRow.GetCell(indexColumns).CellStyle = style6;
                sheet.SetColumnWidth(indexColumns, 8000);//ajusta largura das colunas
                indexColumns++;
            });
            sheet.CreateFreezePane(0, 1, 0, 1);

            //int rowNumber = 1;
            //foreach (var datarow in list)
            //{

            //    var row = sheet.CreateRow(rowNumber++);

            //    row.CreateCell(0).CellStyle.Alignment = HorizontalAlignment.Justify;
            //    row.CreateCell(0).CellStyle.WrapText = false;

            //    row.CreateCell(0).SetCellValue(datarow.);
            //    //row.CreateCell(1).SetCellValue(datarow.dtPecSolicitado);
            //}


            int rowNumber = 1;
            PropertyInfo[] propInfos = typeof(T).GetProperties();
            for (int i = 0; i <= list.Count - 1; i++)
            {
                T item = list[i];
                var items = new List<string>();
                for (int j = 0; j <= propInfos.Length - 1; j++)
                {
                    object o = item.GetType().GetProperty(propInfos[j].Name).GetValue(item, null);
                    string value = "";

                    if (o == null)
                    {
                        value = "-";
                    }
                    else
                    {
                        value = o.ToString();
                    }

                    if (value.Contains("\r"))
                    {
                        value = value.Replace("\r", " ");
                    }
                    if (value.Contains("\n"))
                    {
                        value = value.Replace("\n", " ");
                    }
                    //items.Add(value);

                var row = sheet.CreateRow(rowNumber++);
                row.CreateCell(0).CellStyle.Alignment = HorizontalAlignment.Justify;
                row.CreateCell(0).CellStyle.WrapText = false;

                row.CreateCell(i).SetCellValue(value);

                }
                //report.Rows.Add(items.ToArray());
            }




            MemoryStream output = new MemoryStream();
            workbook.Write(output);
           // return File(output.ToArray(), "application/vnd.ms-excel", "SOC_SUPRIMENTOS.xls");

           
            return output;

      
        }
















        public static void ExportCSV(string csv, string filename)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}.csv", filename));
            HttpContext.Current.Response.ContentType = "text/csv";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
            HttpContext.Current.Response.AddHeader("Pragma", "public");
            HttpContext.Current.Response.Write(csv);
            HttpContext.Current.Response.End();
        }

        public static void ExportCSV<T>(this List<T> list, string filename)
        {
            string csv = GetCSV(list);

            CSVHelperExtension.ExportCSV(csv, filename);
        }


        /// <summary>
        /// Exporta para excel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string GetCSV<T>(this List<T> list)
        {
            StringBuilder sb = new StringBuilder();

            //Get the properties for type T for the headers
            PropertyInfo[] propInfos = typeof(T).GetProperties();
            for (int i = 0; i <= propInfos.Length - 1; i++)
            {
                sb.Append(propInfos[i].Name);

                if (i < propInfos.Length - 1)
                {
                    sb.Append(",");
                }
            }

            sb.AppendLine();

            //Loop through the collection, then the properties and add the values
            for (int i = 0; i <= list.Count - 1; i++)
            {
                T item = list[i];
                for (int j = 0; j <= propInfos.Length - 1; j++)
                {
                    object o = item.GetType().GetProperty(propInfos[j].Name).GetValue(item, null);
                    if (o != null)
                    {
                        string value = o.ToString();

                        //Check if the value contans a comma and place it in quotes if so
                        if (value.Contains(","))
                        {
                            value = string.Concat("\"", value, "\"");
                        }

                        //Replace any \r or \n special characters from a new line with a space
                        if (value.Contains("\r"))
                        {
                            value = value.Replace("\r", " ");
                        }
                        if (value.Contains("\n"))
                        {
                            value = value.Replace("\n", " ");
                        }

                        sb.Append(value);
                    }

                    if (j < propInfos.Length - 1)
                    {
                        sb.Append(",");
                    }
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
