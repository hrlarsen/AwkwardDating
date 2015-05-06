<?php
include_once("php.php");
if(!empty($_POST))
{
    $secret = "PCJzWZKr*k!#%hYX2gMp";
    $post_secret = mysql_real_escape_string($_POST["secret"], $database);


    if($secret == $post_secret) {
        $result = mysql_query("SELECT * FROM AwkwardDate_UserInput ORDER BY UserID");
        $data = array();
        while($row = mysql_fetch_array($result))
        {
            $data_entry = array();
            $data_entry["id"] = intval($row["UserID"]);
            $data_entry["x"] = floatval($row["MoveX"]);
            $data_entry["y"] = floatval($row["MoveY"]);
            array_push($data, $data_entry);
        }
        echo json_encode($data);
        mysql_query("DELETE FROM AwkwardDate_UserInput ");
    }


}
?>