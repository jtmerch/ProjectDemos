using System;

namespace MicroserviceDemo
{

    /*
     Instead of:
    docker build -t microservicedemo .
    Try:
    docker build -t microservicedemo -f Dockerfile ..
    when facing file not found issues on docker build


    RUNNING IMAGE:
    docker run -it --rm -p 3000:80 --name microservicedemocontainer microservicedemo

    YAML:
    container should actually be the container name
    */
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }
}
