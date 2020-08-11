xquery version "3.1";

declare namespace p1 = "http://ftn.uns.ac.rs/xml2019/workflow";

for $workflow in fn:doc("/db/test/workflows.xml")/workflows/p1:workflow
where $workflow/p1:publicationId = "%s"
return $workflow