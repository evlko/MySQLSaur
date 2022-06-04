<?php

    $con = mysqli_connect('localhost', 'root', 'root', 'unityaccess');
    
    if (mysqli_connect_errno())
    {
        echo "1: Connection Failed";
        exit();
    }

    $max_score_query = "SELECT MAX(highscore) FROM players;";
    
    $score_check = mysqli_query($con, $max_score_query) or die("999: Username check failed");
        
    $info = mysqli_fetch_assoc($score_check);
    
    echo "0\t" . $info["MAX(highscore)"];
    
?>