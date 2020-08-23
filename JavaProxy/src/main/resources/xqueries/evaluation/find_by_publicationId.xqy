xquery version "3.1";

declare namespace e1 = "http://ftn.uns.ac.rs/xml2019/evaluation";


let $evaluations := for $evaluation in fn:doc("/db/test/evaluations.xml")/evaluations/e1:evaluation
where $evaluation/@e1:publicationId = "%s"
return $evaluation

return <evaluations> {$evaluations} </evaluations>