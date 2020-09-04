xquery version "3.1";

declare namespace p1 = "http://ftn.uns.ac.rs/xml2019/coverletter";

for $coverletter in fn:doc("/db/test/coverletters.xml")/coverLetters/p1:coverLetter
where $coverletter/@p1:publicationId = "%s"
return $coverletter