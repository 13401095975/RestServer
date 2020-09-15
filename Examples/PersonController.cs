using RestServer.Common.Logger;
using RestServer.Common.Serializer;
using RestServer.Http;
using RestServer.RestAttribute;
using System;
using System.Collections.Generic;

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
            //Person s = JsonSerializer.FromJson<Person>(request.Content);
            //personService.Create(s);
            return "ok";

        }
    }
}
