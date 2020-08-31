xquery version "3.1";

declare namespace p1 = "http://ftn.uns.ac.rs/xml2019/user";

let $users := for $user in fn:doc("/db/test/users.xml")/users/p1:user
where $user/p1:role != "Reviewer"
return $user

return <users> {$users} </users>