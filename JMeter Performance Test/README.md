# JMeter Performance Test

Subject: [Svelte.dev](https://svelte.dev/)

## 7 Routes Tested:
- /
- /tutorial
- /docs
- /examples
- /repl
- /blog
- /faq

## Method
Created the test plan using the JMeter UI. Afterwards, ran the test and generated a report via JMeter CLI
[Link to JMeter Test Plan JMX file]()

Using the CLI to run the test:
{Screenshot here}

[Full JMeter Dashboard Report](https://kevinle108.github.io/jmeter-report/)

## Findings
The website performed well underload. Sent 16,500 requests with no failures. Two of the routes, "/tutorial" and "/repl", made their own network requests that were logged by JMeter. 
