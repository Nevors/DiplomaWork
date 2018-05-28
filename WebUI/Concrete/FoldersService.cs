using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Abstract;
using WebUI.Models;

namespace WebUI.Concrete {
    public class FoldersService : IFoldersService {
        static List<Folder> folders = new List<Folder>();

        public ICollection<Folder> Folders => folders;

    }
}
