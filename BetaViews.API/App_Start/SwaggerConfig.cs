using System.Web.Http;
using WebActivatorEx;
using BetaViews.API;
using Swashbuckle.Application;
using Swashbuckle.Swagger;
using System;
using System.Web.Http.Description;
using System.Text;
using System.Text.RegularExpressions;
using BetaViews.API.Filters;
using System.Collections.Generic;
using System.Linq;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace BetaViews.API
{


    public class CsrfDocumentFiler : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.operationId != "Token_GetToken")
            {
                if (operation.parameters == null)
                    operation.parameters = new List<Parameter>();

                operation.parameters.Add(new Parameter
                {
                    name = "Authorization",
                    @in = "header",
                    type = "string",
                    required = true
                });
            };
        }
    }
    
    /// <summary>
    /// swagger
    /// </summary>
    public class SwaggerConfig
    {
        /// <summary>
        /// comentarios
        /// </summary>
        /// <returns></returns>
        protected static string GetXmlCommentsPath()
        {
            return System.String.Format(@"{0}\bin\BetaViews.API.XML", System.AppDomain.CurrentDomain.BaseDirectory);
        }


        private string Formatted(string text)
        {
            if (text == null) return null;

            // Strip out the whitespace that messes up the markdown in the xml comments,
            // but don't touch the whitespace in <code> blocks. Those get fixed below.
            string resultString = Regex.Replace(text, @"(^[ \t]+)(?![^<]*>|[^>]*<\/)", "", RegexOptions.Multiline);
            resultString = Regex.Replace(resultString, @"<code[^>]*>", "<pre>", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline);
            resultString = Regex.Replace(resultString, @"</code[^>]*>", "</pre>", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline);
            resultString = Regex.Replace(resultString, @"<!--", "", RegexOptions.Multiline);
            resultString = Regex.Replace(resultString, @"-->", "", RegexOptions.Multiline);

            try
            {
                string pattern = @"<pre\b[^>]*>(.*?)</pre>";

                foreach (Match match in Regex.Matches(resultString, pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline))
                {
                    var formattedPreBlock = FormatPreBlock(match.Value);
                    resultString = resultString.Replace(match.Value, formattedPreBlock);
                }
                return resultString;
            }
            catch
            {
                // Something went wrong so just return the original resultString
                return resultString;
            }
        }

        private string FormatPreBlock(string preBlock)
        {
            // Split the <pre> block into multiple lines
            var linesArray = preBlock.Split('\n');
            if (linesArray.Length < 2)
            {
                return preBlock;
            }
            else
            {
                // Get the 1st line after the <pre>
                string line = linesArray[1];
                int lineLength = line.Length;
                string formattedLine = line.TrimStart(' ', '\t');
                int paddingLength = lineLength - formattedLine.Length;

                // Remove the padding from all of the lines in the <pre> block
                for (int i = 1; i < linesArray.Length - 1; i++)
                {
                    linesArray[i] = linesArray[i].Substring(paddingLength);
                }

                var formattedPreBlock = string.Join("", linesArray);
                return formattedPreBlock;
            }

        }





        /// <summary>
        /// registro do swagger
        /// </summary>
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration 
                .EnableSwagger(c =>
                    {
                        
                        c.SingleApiVersion("v1", "BetaViews.API");
                        c.IncludeXmlComments(GetXmlCommentsPath());                        

                        c.ApiKey("Authorization")
                            .Description("API Key Authentication")
                            .Name("Authorization")
                            .In("header");
                        c.DescribeAllEnumsAsStrings();
                        c.OperationFilter<CsrfDocumentFiler>();                        
                    })
                .EnableSwaggerUi(c =>
                    {                                                
                       c.EnableApiKeySupport("Authorization", "header");
                        c.DisableValidator();
                    });
        }
    }
}
