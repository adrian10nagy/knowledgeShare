using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softvision.BL.TrueEditor
{
    public static class TrueEditor
    {
        public static string GenerateHTML(string innerRep)
        {
            //innerRep = "<p>" + innerRep + "</p>";
            innerRep = innerRep.Replace("\t", "&emsp;");
            innerRep = innerRep.Replace("\n", "<br>");
            //innerRep = innerRep.Replace("  ", "&#160 ");
            //innerRep = innerRep.Replace("  ", "&nbsp; ");
            
            var hasReplaces = true;
            int startIndexCode;
            int endIndexCode;

            int startIndexBold;
            int endIndexBold;

            int startIndexItalic;
            int endIndexItalic;

            int startIndexChapter;
            int endIndexChapter;

            int startIndexSubChapter;
            int endIndexSubChapter;

            int startIndexParafraph;
            int endtIndexParagraph;

            while (hasReplaces)
            {
                hasReplaces = false;
                startIndexCode = innerRep.IndexOf("~~");
                endIndexCode = innerRep.IndexOf("~~", startIndexCode+2);
                if (startIndexCode != -1 && endIndexCode != -1)
                {
                    innerRep = innerRep.Substring(0, startIndexCode) + "<div class='codeText'>" + innerRep.Substring(startIndexCode + 2);
                    endIndexCode = innerRep.IndexOf("~~", startIndexCode+2);
                    innerRep = innerRep.Substring(0, endIndexCode) + "</div>" + innerRep.Substring(endIndexCode + 2);
                    hasReplaces = true;
                }

                startIndexBold = innerRep.IndexOf("**");
                endIndexBold = innerRep.IndexOf("**", startIndexBold + 2);
                if (startIndexBold != -1 && endIndexBold != -1)
                {
                    innerRep = innerRep.Substring(0, startIndexBold) + "<strong>" + innerRep.Substring(startIndexBold + 2);
                    endIndexBold = innerRep.IndexOf("**", startIndexBold + 2);
                    innerRep = innerRep.Substring(0, endIndexBold) + "</strong>" + innerRep.Substring(endIndexBold + 2);
                    hasReplaces = true;
                }

                startIndexItalic = innerRep.IndexOf("!!");
                endIndexItalic = innerRep.IndexOf("!!", startIndexItalic + 2);
                if (startIndexItalic != -1 && endIndexItalic != -1)
                {
                    innerRep = innerRep.Substring(0, startIndexItalic) + "<i>" + innerRep.Substring(startIndexItalic + 2);
                    endIndexItalic = innerRep.IndexOf("!!", startIndexItalic +2);
                    innerRep = innerRep.Substring(0, endIndexItalic) + "</i>" + innerRep.Substring(endIndexItalic + 2);
                    hasReplaces = true;
                }

                startIndexChapter = innerRep.IndexOf("#2");
                endIndexChapter = innerRep.IndexOf("#2", startIndexChapter + 2);
                if (startIndexChapter != -1 && endIndexChapter != -1)
                {
                    innerRep = innerRep.Substring(0, startIndexChapter) + "<h2>" + innerRep.Substring(startIndexChapter + 2);
                    endIndexChapter = innerRep.IndexOf("#2", startIndexChapter + 2);
                    innerRep = innerRep.Substring(0, endIndexChapter) + "</h2>" + innerRep.Substring(endIndexChapter + 2);
                    hasReplaces = true;
                }

                startIndexSubChapter = innerRep.IndexOf("#3");
                endIndexSubChapter = innerRep.IndexOf("#3", startIndexSubChapter + 2);
                if (startIndexSubChapter != -1 && endIndexSubChapter != -1)
                {
                    innerRep = innerRep.Substring(0, startIndexSubChapter) + "<h3>" + innerRep.Substring(startIndexSubChapter + 2);
                    endIndexSubChapter = innerRep.IndexOf("#3", startIndexSubChapter + 2);
                    innerRep = innerRep.Substring(0, endIndexSubChapter) + "</h3>" + innerRep.Substring(endIndexSubChapter + 2);
                    hasReplaces = true;
                }

                startIndexParafraph = innerRep.IndexOf("##");
                endtIndexParagraph = innerRep.IndexOf("##", startIndexParafraph + 2);
                if (startIndexParafraph != -1 && endtIndexParagraph != -1)
                {
                    innerRep = innerRep.Substring(0, startIndexParafraph) + "<p>" + innerRep.Substring(startIndexParafraph + 2);
                    endtIndexParagraph = innerRep.IndexOf("##", startIndexParafraph + 2);
                    innerRep = innerRep.Substring(0, endtIndexParagraph) + "</p>" + innerRep.Substring(endtIndexParagraph + 2);
                    hasReplaces = true;
                }


                startIndexCode = endIndexCode = startIndexBold = endIndexBold = 
                    startIndexItalic = endIndexItalic = startIndexChapter = 
                    endIndexChapter = startIndexSubChapter = endIndexSubChapter =
                    endtIndexParagraph = endtIndexParagraph = 0;
            }

            return innerRep;
        }
    }
}
