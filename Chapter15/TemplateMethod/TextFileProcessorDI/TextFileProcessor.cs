using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFileProcessorDI {
    public class TextFileProcessor {
        private ITextFileService _textFileService;

        public TextFileProcessor(ITextFileService service) {
            _textFileService = service;
        }

        public void Run(string fileName){
            _textFileService.Initialize(fileName);
            var lines = File.ReadLines(fileName);
            foreach (var line in lines) {
                _textFileService.Execute(line);
            }
            _textFileService.Terminate();
        } 
    }
}
