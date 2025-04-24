using Autodesk.Revit.DB;
using NUnit.Framework;
using RevitTestStorage.Tests.Services;
using RevitTestStorage.Tests.Utils;
using System;
using System.Collections.Generic;

namespace RevitTestStorage.Tests
{
    public class StorageMaxRevitTests : OneTimeOpenDocumentTest
{
        [TestCase(1e3)]
        [TestCase(1e6)]
        [TestCase(1e7)]
        [TestCase(16777216)]
        public void CreateStorage(long length)
        {
            IStorageProjectInfo storageProjectInfo = new StorageProjectInfo();

            using (Transaction transaction = new Transaction(document))
            {
                transaction.Start("Save");
                string value = new string('a', (int)length);
                storageProjectInfo.Save(document, value);
                transaction.Commit();
            }
        }

        [TestCase(16777217)] // Autodesk.Revit.Exceptions.ArgumentException: String is too long; 16777217 exceeds max length of 16mb characters.
        public void CreateStorage_Exception(long length)
        {
            IStorageProjectInfo storageProjectInfo = new StorageProjectInfo();

            using (Transaction transaction = new Transaction(document))
            {
                transaction.Start("Save");
                string value = new string('a', (int)length);
                Assert.Catch<Autodesk.Revit.Exceptions.ArgumentException>(() => storageProjectInfo.Save(document, value));
                transaction.Commit();
            }
        }

        [TestCase(1e3)]
        [TestCase(1e5)]
        [TestCase(3e5)]
        public void CreateStorage_Json(long length)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();

            for (int i = 0; i < length; i++)
            {
                data[i.ToString()] = Guid.NewGuid().ToString();
            }

            IStorageProjectInfo storageProjectInfo = new StorageProjectInfo();

            using (Transaction transaction = new Transaction(document))
            {
                transaction.Start("Save");
                var json = data.ToJson();
                System.Console.WriteLine(json.Length);
                storageProjectInfo.Save(document, json);
                transaction.Commit();
            }
        }
    }
}
