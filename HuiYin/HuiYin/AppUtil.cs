using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Web.Mvc;
using Microsoft.Office.Interop.Word;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MSWord = Microsoft.Office.Interop.Word;
namespace HuiYin
{
    public class AppUtil
    {

        public static SelectList NullSelectList
        {
            get
            {
                var resultList = new SelectList(new List<SelectListItem>(), "Value", "Text");
                return resultList;
            }
        }




        #region --获取pdf文件的页数--
        public int GetPDFPageCountByDll(string path)
        {
            // 创建一个PdfReader对象
            PdfReader reader = new PdfReader(path);
            // 获得文档页数
            int pagecount = reader.NumberOfPages;
            return pagecount;
        }
        /// <summary>
        /// 获取pdf文件的页数
        /// </summary>
        ///
        public int GetPDFPageCount(string path)
        {
            byte[] buffer = File.ReadAllBytes(path);
            int length = buffer.Length;
            if (buffer == null)
                return -1;
            if (buffer.Length <= 0)
                return -1;
            try
            {
                int i = 0;
                int nPos = BytesLastIndexOf(buffer, length, "/Type/Pages");
                if (nPos == -1)
                    return -1;
                string pageCount = null;
                for (i = nPos; i < length - 10; i++)
                {
                    if (buffer[i] == '/' && buffer[i + 1] == 'C' && buffer[i + 2] == 'o' && buffer[i + 3] == 'u' && buffer[i + 4] == 'n' && buffer[i + 5] == 't')
                    {
                        int j = i + 3;
                        while (buffer[j] != '/' && buffer[j] != '>')
                            j++;
                        pageCount = Encoding.Default.GetString(buffer, i, j - i);
                        break;
                    }
                }
                if (pageCount == null)
                    return -1;
                int n = pageCount.IndexOf("Count");
                if (n > 0)
                {
                    pageCount = pageCount.Substring(n + 5).Trim();
                    for (i = pageCount.Length - 1; i >= 0; i--)
                    {
                        if (pageCount[i] >= '0' && pageCount[i] <= '9')
                        {
                            return int.Parse(pageCount.Substring(0, i + 1));
                        }
                    }
                }
                return -1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        private int BytesLastIndexOf(Byte[] buffer, int length, string Search)
        {
            if (buffer == null)
                return -1;
            if (buffer.Length <= 0)
                return -1;
            byte[] SearchBytes = Encoding.Default.GetBytes(Search.ToUpper());
            for (int i = length - SearchBytes.Length; i >= 0; i--)
            {
                bool bFound = true;
                for (int j = 0; j < SearchBytes.Length; j++)
                {
                    if (ByteUpper(buffer[i + j]) != SearchBytes[j])
                    {
                        bFound = false;
                        break;
                    }
                }
                if (bFound)
                    return i;
            }
            return -1;
        }
        private byte ByteUpper(byte byteValue)
        {
            char charValue = Convert.ToChar(byteValue);
            if (charValue < 'a' || charValue > 'z')
                return byteValue;
            else
                return Convert.ToByte(byteValue - 32);
        }
        #endregion


        private MSWord.Application wordApp;  //Word应用程序变量
        private MSWord.Document wordDoc;     //Word文档变量
        private Object Nothing = Missing.Value;

        #region --获取word文件的页数--
        /// <summary>
        /// 获得word文档页数
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public int GetWordPageCount(string path)
        {
            int p = 0;
//            Selection ss=new Selection();
            //            FileInfo info=new FileInfo(path);
            //            var ss = info.Attributes;

            wordApp = new MSWord.ApplicationClass();
            wordDoc = wordApp.Documents.Add(ref Nothing, ref Nothing, ref Nothing, ref Nothing);
            //打开Word
object FileName = path;
            object readOnly = false;
            object isVisible = true;
            wordDoc = wordApp.Documents.Open(ref FileName, ref Nothing, ref readOnly,
            ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing,
            ref Nothing, ref Nothing, ref Nothing, ref isVisible, ref Nothing,
            ref Nothing, ref Nothing, ref Nothing);

            // 计算Word文档页数
            MSWord.WdStatistic stat = MSWord.WdStatistic.wdStatisticPages;
            int num = wordDoc.ComputeStatistics(stat, ref Nothing);


            try
            {
                Microsoft.Office.Interop.Word.Application myWordApp = new Microsoft.Office.Interop.Word.ApplicationClass();
                object oMissing = System.Reflection.Missing.Value;

                myWordApp.Visible = false;
                object filePath = path; //这里是Word文件的路径
                //打开文件
                Microsoft.Office.Interop.Word.Document myWordDoc = myWordApp.Documents.Open(
                    ref filePath, ref oMissing, ref oMissing, ref oMissing,
                    ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                    ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                    ref oMissing, ref oMissing, ref oMissing, ref oMissing);
                //下面是取得打开文件的页数
                int pages = myWordDoc.ComputeStatistics(WdStatistic.wdStatisticPages, ref oMissing);
                myWordDoc.Close(ref oMissing, ref oMissing, ref oMissing);

                if (myWordApp != null)
                {
                    //关闭文件
                    ((Microsoft.Office.Interop.Word.Application)myWordApp).Quit(ref oMissing, ref oMissing, ref oMissing);
                }

                p = pages;
            }
            catch (Exception ex)
            {

            }
            
            return p;
        }

        #endregion 
    }

}