xquery version "3.1";

declare namespace p1 = "http://ftn.uns.ac.rs/xml2019/publication";
declare namespace w1 = "http://ftn.uns.ac.rs/xml2019/workflow";

let $publicationIds := for $workflow in fn:doc("/db/test/workflows.xml")/workflows/w1:workflow
where $workflow/w1:reviewers/w1:reviewer = "%s"
return $workflow/w1:publicationId

let $publications := for $publication in fn:doc("/db/test/publications.xml")/publications/p1:publication
where $publication/@p1:id = $publicationIds and $publication/p1:header/p1:status != "withdrawn"
return $publication

return <publications> {$publications} </publications>