﻿using Minifying.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Minifying.Abstract;
using System.Threading;

namespace Minifying.Concrete.Common {
    class ValueProvider : IValueProvider{
        private readonly List<IOutputMessage> outputMessages = new List<IOutputMessage>();
        Dictionary<FileType, Dictionary<string, File>> files = new Dictionary<FileType, Dictionary<string, File>>();
        object obj = new object();

        public void AddFile(File file) {
            Dictionary<string, File> dic;

            Monitor.Enter(obj);

            if (!files.ContainsKey(file.Type)) {
                dic = new Dictionary<string, File>();
                files.Add(file.Type, dic);
            } else {
                dic = files[file.Type];
            }
            dic.Add(file.SearchName, file);

            Monitor.Exit(obj);
        }

        public File GetFile(string fileName) {
            foreach (var item in files.Values) {
                if (item.ContainsKey(fileName)) {
                    return item[fileName];
                }
            }
            return null;
        }
        public List<File> GetFiles(FileType type) {
            if (files.ContainsKey(type)) {
                return files[type].Values.ToList();
            }
            return new List<File>();
        }

        public List<File> GetFiles() {
            return files.SelectMany(i => i.Value.Values).ToList();
        }

        public List<IOutputMessage> GetOutputMessages() {
            return outputMessages;
        }

        public void RemoveFile(string fileName) {
            foreach (var item in files.Values) {
                if (item.ContainsKey(fileName)) {
                    item.Remove(fileName);
                }
            }
        }
    }
}
