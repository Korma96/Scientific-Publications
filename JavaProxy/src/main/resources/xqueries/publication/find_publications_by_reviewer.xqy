xquery version "3.1";

declare namespace p1 = "http://ftn.uns.ac.rs/xml2019/publication";

let $publicationIds := for $workflow in fn:doc("/db/test/workflows.xml")/workflows/workflow
                      where $workflow/reviewers/reviewer = "%s"
                      return $workflow/publicationId

for $publication in fn:doc("/db/test/publications.xml")/publications/p1:publication
where $publication/@p1:id = $publicationIds and $publication/p1:header/p1:status != "deleted"
return $publication

return <publications> {$publications} </publications>