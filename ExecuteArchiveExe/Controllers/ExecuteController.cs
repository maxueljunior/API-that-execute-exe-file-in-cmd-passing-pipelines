using System.Diagnostics;
using ExecuteArchiveExe.Contract.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ExecuteArchiveExe.Controllers;

[ApiController]
[Route("[controller]")]
public class ExecuteController : ControllerBase{

    private readonly IWebHostEnvironment _env;

    public ExecuteController(IWebHostEnvironment env){
        _env = env;
    }

    [HttpPost]
    public IActionResult ExecuteArchive(ExecuteArchiveRequest request){
        Console.Write("cHEGOU Aqui");
        RunExecutable(request.parametros);
        return Ok();
    } 

    [HttpPost("/teste")]
    public IActionResult testeDeExecucao(){
        string contentRootPath = _env.ContentRootPath;
        string webRootPath = _env.WebRootPath;  

        string executablePath = "console23.exe";
        string inputFileName = "console23.in";
        string outputFileName = "console23.out";

        // Diretório onde o executável está localizado
        string executableDirectory = contentRootPath + webRootPath + @"\Content";

        var fileName = contentRootPath + webRootPath + @"\Content\console23.exe";

        //string cmdCommand = $"cd \"{contentRootPath}\" && \"\" < \"console23.in\" > \"console23.out\"";
        string cmdCommand = $"cd \"{executableDirectory}\" && \"{executablePath}\" < \"{inputFileName}\" > \"{outputFileName}\"";
        ProcessStartInfo psi = new ProcessStartInfo("cmd.exe")
        {
            RedirectStandardInput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        Process process = new Process
        {
            StartInfo = psi
        };

        process.Start();


        process.StandardInput.WriteLine(cmdCommand);

        process.StandardInput.WriteLine("exit");
        process.WaitForExit();
        process.Close();

        Console.WriteLine("Execução concluída!");

        return Ok();
    }

    private void RunExecutable(string parametros){
        try{
            string contentRootPath = _env.ContentRootPath;
            string webRootPath = _env.WebRootPath;

            var fileName = contentRootPath + webRootPath + @"\Content\console23.exe";

            Process p = new Process();
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.FileName = fileName;
            p.StartInfo.Arguments = parametros;
            p.StartInfo.WorkingDirectory = contentRootPath + webRootPath + @"\Content\";
            p.Start();
            p.WaitForExit();

        }catch(Exception)
        {

        }
    }

}

