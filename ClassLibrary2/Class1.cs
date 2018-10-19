using System;
using System.Windows;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Runtime.Serialization.Formatters.Binary;
namespace MegaLib
{
    /// <summary>
    /// Get and save data in binary files
    /// </summary>
    public static class IO
    {
        /// <summary>
        /// Get data from binary file
        /// </summary>
        /// <typeparam name="T">Any type</typeparam>
        /// <param name="fileAddress">
        /// the address of file which contains getting data in the storge
        /// </param>
        /// <returns>
        /// A object has T type
        /// </returns>
        public static T GetData<T>(string fileAddress)
        where T : class, new()
        {
            if (File.Exists(fileAddress))
            {
                Stream file = File.Open(fileAddress, FileMode.OpenOrCreate);
                T data = (new BinaryFormatter()).Deserialize(file) as T;
                file.Close();
                return data;
            }
            else
            {
                T data = new T();
                SetData(fileAddress, data);
                return data;
            }
        }
        /// <summary>
        /// Set data from binary file
        /// </summary>
        /// <param name="fileAddress">the address of file which contains setting data in the storge</param>
        /// <param name="data">the setting data</param>
        public static void SetData(string fileAddress, object data)
        {
            Stream file = File.Open(fileAddress, FileMode.OpenOrCreate);
            (new BinaryFormatter()).Serialize(file, data);
            file.Close();
        }
    }
    public static class DocQuery
    {
        public static FlowDocument DocumnetReader(string documentAddress)
        {
            FlowDocument Content = new FlowDocument();
            TextRange textRange = new TextRange(Content.ContentStart, Content.ContentEnd);
            System.IO.FileStream fileStream = new System.IO.FileStream(documentAddress, System.IO.FileMode.Open);
            textRange.Load(fileStream, DataFormats.Rtf);
            fileStream.Close();
            return Content;
        }
        public static void DocumentWriter(FlowDocument flowDocument, string documentAddress)
        {
            FileStream file = File.Open(documentAddress, FileMode.OpenOrCreate);
            (new TextRange(flowDocument.ContentStart, flowDocument.ContentEnd)).Save(file, DataFormats.Rtf);
            file.Close();
        }
        public static bool IsNullOrEmpty(this FlowDocument Content)
        {
            if (Content.Blocks.Count == 0) return true;
            TextPointer startIndex = Content.ContentStart.GetInsertionPosition(LogicalDirection.Forward);
            TextPointer endIndex = Content.ContentEnd.GetInsertionPosition(LogicalDirection.Backward);
            if (startIndex.CompareTo(endIndex) == 0) return true;
            return false;
        }
    }
    //public static class Sort
    //{
    //    static void a()
    //    {
    //        IO.GetData<Math>("djsdjsd")
    //    }
    //}
}
