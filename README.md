# Scientific-Publications
Project for XML and web services course


## Description
  1. Project specification [link](https://github.com/Korma96/Scientific-Publications/blob/development/doc/ProjectSpecification.pdf)
  2. Architecture [link](https://github.com/Korma96/Scientific-Publications/blob/development/doc/architecture.jpg)
  3. Use Case diagram [link](https://github.com/Korma96/Scientific-Publications/blob/development/doc/use_case_diagram.jpg)
  4. State diagram [link](https://github.com/Korma96/Scientific-Publications/blob/development/doc/state_diagram.jpg)

## Prerequisites
  - Java 8
  - .NET Core SDK 2.1
  - Apache Tomee
  - fuseki.war and exist.war are present in webapps folder
 
## Setup

1. Clone project

2. Run Tomcat server
    
3. Setup database
    - Navigate to http://localhost:8080/exist/apps/dashboard/index.html
    - create test collection and upload all .xml documents from ScientificPublications/resources/db
    ![alt text](https://github.com/Korma96/Scientific-Publications/blob/development/doc/exist_documents.jpg)
    
4. Run java backend
    - set current working directory on ScientificPublications
    - java -jar .\JavaProxy\target\JavaProxy-0.0.1-SNAPSHOT.jar .\target\classes\com\xml\JavaProxy\JavaProxyApplication

5. Run .net backend
    - set current working directory on ScientificPublications
    - dotnet run -p .\Backend\ScientificPublications
  
6. Run frontend
    - navigate to https://localhost:5001/index.html

## Demo

Short video demonstrates "happy flow" and shortest path
through publication publish process.

Youtube [link](https://youtu.be/FVLTCo4onAk)
