xquery version "3.1";

declare namespace p1 = "http://ftn.uns.ac.rs/xml2019/workflow";

let $workflows := for $workflow in fn:doc("/db/test/workflows.xml")/workflows/p1:workflow
where $workflow/p1:reviewers/p1:reviewer = "%s"
return $workflow

return <workflows> {$workflows} </workflows>