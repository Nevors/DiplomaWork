#pragma warning disable CS4014

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minifying;
using Minifying.External.Abstract;
using Minifying.External.Models;
using WebUI.Abstract;
using WebUI.Models;
using static WebUI.Tools.Tools;

namespace WebUI.Controllers {
    public class FoldersController : Controller {
        private readonly IHostingEnvironment appEnv;
        private readonly IArchiveManager archiveManager;
        private readonly IFoldersService foldersService;

        public FoldersController(IHostingEnvironment appEnv,
                                    IArchiveManager archiveManager,
                                    IFoldersService foldersService) {
            this.appEnv = appEnv;
            this.archiveManager = archiveManager;
            this.foldersService = foldersService;
        }
        [HttpGet]
        public IActionResult Upload() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file, MinimizationType opt) {
            // путь к папке Temp
            string name = GetRandomString(20);

            var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            ms.Position = 0;
            Dictionary<string, Stream> files = null;
            try {
                files = archiveManager.Uncompress(ms);
            } catch {
                return BadRequest("Не удалось разобрать архив");
            }

            var manager = new Manager(files);

            var folder = new Folder {
                Id = name,
                Files = files,
                IsProcessing = true,
                Manager = manager
            };
            foldersService.Folders.Add(folder);

            Task.Run(() => {
                return manager.ToMinimize(opt);
            }).ContinueWith((result) => {
                var findFolder = foldersService.Folders.FirstOrDefault(i => i.Id == name);
                findFolder.Messages = result.Result;
                findFolder.IsProcessing = false;
            });

            return RedirectToRoute(new { id = name });
        }

        [HttpGet]
        public IActionResult ViewFolder(string id) {
            var folder = foldersService.Folders.FirstOrDefault(i => i.Id == id);
            if (folder == null) {
                return NotFound();
            }

            var answer = new Answer {
                Id = id,
                IsProcessing = folder.IsProcessing,
                Messages = folder.Messages
            };

            return View(answer);
        }

        [HttpPost]
        public IActionResult Download(string id, int[] answers) {
            var folder = foldersService.Folders.FirstOrDefault(i => i.Id == id);
            var listAnswers = answers
                .Where(i => i != -1)
                .Select((i, index) => new Minifying.External.Models.Answer<AnswerType> {
                    Id = index,
                    Value = (AnswerType)i
                })
                .ToList();
            var files = folder.Manager.GetFiles(listAnswers);
            var result = archiveManager.Compress(files);

            return File(result, "application/zip");
        }
    }
}