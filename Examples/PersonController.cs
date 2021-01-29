using HttpMultipartParser;
using RestServer.Common.Logger;
using RestServer.Common.Serializer;
using RestServer.Http;
using RestServer.RestAttribute;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Examples
{
    [Component("PersonController")]
    public class PersonController
    {
        [Autowired]
        public PersonService personService;
        
        private ILogger logger = LoggerFactory.GetLogger();
       
        [RequestMapping("GET","/api/person/list")]
        public List<Person> GetPersonList()
        {
            return personService.GetPersonList();
        }

        [RequestMapping("GET", "/api/person")]
        public Person GetPerson([RequestParam("id")]int id)
        {
            logger.Debug("id:"+id);
            return personService.GetPerson((int)id);
        }
        [RequestMapping("POST", "/api/person")]
        public string Create([RequestBody] Person person)
        {
            logger.Info("person:" + person.ToString());
            return "ok";

        }
        [RequestMapping("POST", "/api/upload")]
        public string Upload(HttpRequest request)
        {
            List<ParameterPart> Parameters = request.MultipartFormData.Parameters;
            Parameters.ForEach(x =>
            {
                logger.Info(x.Name + ":" + x.Data);
            });

            List<FilePart> files = request.MultipartFormData.Files;
            files.ForEach(x =>
            {
                logger.Info(x.Name + ":" + x.FileName);
                File.WriteAllBytes(x.FileName, x.Data);
            });
            
            return "ok";

        }
    }
}
