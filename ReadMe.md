该项目是参考Java SpringMVC框架使用C#语言编写的简易RestApi服务器框架，目前支持：

- 静态页面处理 
- GET/POST/PUT/DELETE请求 
- 支持返回JSON 
- 支持路由方法 
- 支持自定义过滤器
- 服务器返回数据支持gzip压缩
- 支持Component变量自动注入
- 支持Component自动扫描
- GET/POST支持查询参数，POST支持body数据
- 注解支持
  - Component
  - WebFilter
  - RequestMapping
  - Autowired
  - RequestBody
  - RequestParam

# 快速开始

#### 1. 创建Controller

```c#
    [Component("PersonController")]
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
```

#### 2.创建Service

```c#
    [Component("PersonService")]
    public class PersonService
    {
        private ILogger logger = new ConsoleLogger();

        public List<Person> GetPersonList() {
            return TestData.PersonList;
        }

        public Person GetPerson(int id)
        {
            return TestData.PersonList.Find(x => x.id == id);
        }

        public void Create(Person person)
        {
            logger.Debug(person.ToString());
        }
    }
```

#### 3.启动服务

**controller和service上增加Component注解后，服务启动时会进行自动扫描**

```
class Program
{
    static void Main(string[] args)
    {
        new RestApplicationServer().run();
    }
}
```
