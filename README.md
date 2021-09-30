# ApplicationInsightsAvailabilityAgent
Simple agent to do availability check for Application Insights on a private agent

## Run with docker

```
docker run -e CHECKS__CHECK1__INSTRUMENTATIONKEY=5fd744a8-b8bd-4232-aa2b-577325f6d857 -e CHECKS__CHECK1__METHOD=HTTP -e CHECKS__CHECK1__OPTIONS__URI=https://example.com/health -e CHECKS__CHECK2__INSTRUMENTATIONKEY=8e1f4780-ef00-482b-89a3-c22d0d5c5918 -e CHECKS__CHECK2__METHOD=HTTP -e CHECKS__CHECK2__OPTIONS__URI=https://otherexample.com/health -d jasase/appinsightsavailabilityagent
```

## Configuration

Global configuration parameter. Every parameter is optional.

| Environment variable | Description | Default |
|----------------------|-------------|---------|
| RUNLOCATION          | The location of the execution of the availability check which will be reported to Application Insights |  [Hostname] |


Configuration parameter to configure one availability check. This parameters can be repeated to execute multiple check by changed the name part of the variable. Every parameter is mandantory.

| Environment variable | Description |
|----------------------|-------------|
| CHECKS__[NAME]__INSTRUMENTATIONKEY | Instrumentation key of the Application Insights resource in Azure | 
| CHECKS__[NAME]__METHOD | Type of the availability check. Currently only `HTTP` is available. | 
| CHECKS__[NAME]__OPTIONS__URI | URL which should be checked. | 

## Type of checks

### HTTP

The HTTP checks calls the given URL and checks if the response has a HTTP status code 200 - 299.