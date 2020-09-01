xquery version "3.1";

let $users := for $user in fn:doc("/db/test/users.xml")/users/user
where $user/role != "Reviewer"
return $user

return <users> {$users} </users>