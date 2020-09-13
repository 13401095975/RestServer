using RestServer.Common.Logger;
using RestServer.Common.Serializer;
using RestServer.Http;
using RestServer.RestAttribute;
using System;
using System.Collections.Generic;

namespace Examples
{
    [Component("PersonRoute")]
    public class PersonController
    {
        [Autowired]
        public PersonService personService;
        
        private ILogger logger = new ConsoleLogger();
       
        [RequestMapping("GET","/api/person/list")]
        public List<Person> GetPersonList(HttpRequest request)
        {
            return personService.GetPersonList();
        }

        [RequestMapping("GET", "/api/person")]
        public Person GetPerson(HttpRequest request)
        {
            int? id = request.Query.GetIntValue("id");
            logger.Debug("id:"+id);
            return personService.GetPerson((int)id);
        }
        [RequestMapping("POST", "/api/person")]
        public string Create(HttpRequest request)
        {
            Person s = JsonSerializer.FromJson<Person>(request.Content);
            personService.Create(s);
            return "ok";

        }
    }
}
