using LanchesMac.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc;
using System.Linq;

namespace LanchesMac.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminImagensController : Controller
    {
        private readonly ConfigurationImages _myConfig;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminImagensController(IWebHostEnvironment webHostEnvironment,
            IOptions<ConfigurationImages> myConfig)
        {
            _webHostEnvironment = webHostEnvironment;
            _myConfig = myConfig.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                ViewData["Erro"] = "Erro: Arquivo(s) não selecionado(s)";
                return View(ViewData);
            }

            if (files.Count > 10)
            {
                ViewData["Erro"] = "Erro: Quantidade de arquivos excedeu o limite";
                return View(ViewData);
            }

            var size = (files.Sum(f => f.Length)) / 1024;

            var filePathsName = new List<string>();

            var filePath = Path.Combine(_webHostEnvironment.WebRootPath,
                _myConfig.ProductImagesFolderName);

            foreach (var formFile in files)
            {
                if (formFile.FileName.Contains(".jpg") || 
                    formFile.FileName.Contains(".png") ||
                    formFile.FileName.Contains(".gif"))
                {
                    var fileNameWithPath = string.Concat(filePath, "\\", formFile.FileName);
                    filePathsName.Add(fileNameWithPath);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            ViewData["Resultado"] = $"{files.Count} arquivos foram enviados, " +
                                        $"con total de : {size} MB";   
                
            ViewBag.Arquivos = filePathsName;

            return View(ViewData);
        }

        public IActionResult GetImages()
        {
            FileManagerModel model = new FileManagerModel();

            var userImagePath = Path.Combine(_webHostEnvironment.WebRootPath,
                _myConfig.ProductImagesFolderName);

            DirectoryInfo dir = new DirectoryInfo(userImagePath);
            FileInfo[] files = dir.GetFiles();
            model.PathImagesProduto = _myConfig.ProductImagesFolderName;

            if (files.Length == 0)
            {
                ViewData["Erro"] = $"Nenhum arquivo encontrado na pasta {userImagePath}";
            }

            model.Files = files;

            return View(model);
        }

        public IActionResult Deletefile(string fname)
        {
            var userImagePath = Path.Combine(_webHostEnvironment.WebRootPath,
                _myConfig.ProductImagesFolderName + "\\", fname);

            if (System.IO.File.Exists(userImagePath))
            {
                System.IO.File.Delete(userImagePath);

                ViewData["Deletado"] = $"Arquivo(s) {userImagePath} deletado com sucesso!";
            }

            return View("index");
        }
    }
}
