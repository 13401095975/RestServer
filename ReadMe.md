为了能够在桌面端软件中简单方便的对外提供RestApi接口，参考Java SpringMVC框架使用C#语言编写了一个简易RestApi服务器框架，目前支持：

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



## 快速开始

### 示例一 静态页面映射

```c#
new RestApplicationServer().run(new RestConfiguration { 
     StaticFileConfigurations = new List<StaticFileConfiguration>() { 
         new StaticFileConfiguration("/e", "E:\\"),
         new StaticFileConfiguration("/f", "F:\\")
     }
 });
// 将 http://xxxxx.com/e/xxxxx 映射到本地磁盘文件 E：\\xxxxx
// 将 http://xxxxx.com/f/xxxxx 映射到本地磁盘文件 F：\\xxxxx
```



### 示例二 自定义路由

#### 1.添加Controller

```c#
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
    }
```

#### 2.添加Service

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

### 示例三 增加Filter

#### 1. 添加一个计算接口处理耗时的filter

```
[WebFilter(1, "/")]
    public class StopWacthFilter : IFilter
    {
        public void Filter(HttpRequest request,ref HttpResponse response, ProcessChain chain, int nextIndex)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            chain.doFilter(request,ref response, nextIndex);
            stopwatch.Stop();
            Console.WriteLine(request.Method + " "+request.Path+ ", 耗时："+(stopwatch.ElapsedMilliseconds).ToString()+"ms");
        }
    }
```

**自定义filter上增加WebFilter注解后，服务启动时会进行自动扫描**