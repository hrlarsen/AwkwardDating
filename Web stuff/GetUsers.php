<?php
include_once("php.php");
if(!empty($_POST)) {
    $secret = "PCJzWZKr*k!#%hYX2gMp";
    $post_secret = mysql_real_escape_string($_POST["secret"], $database);


    if ($secret == $post_secret) {

        $time = time();
        $time -= 30;
        mysql_query("DELETE FROM AwkwardDate_Users WHERE LastSeen < '$time'");

        $result = mysql_query("SELECT UserID,Color FROM AwkwardDate_Users");
        $users = array();
        while($row = mysql_fetch_array($result))
        {
            $user = array();
            $user["id"] = intval($row["UserID"]);
            $user["color"] = $row["Color"];
            array_push($users,$user);
        }
        echo json_encode($users);

    }


}


?>