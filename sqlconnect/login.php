<?php

    $con = mysqli_connect('localhost', 'root', 'root', 'unityaccess');
    
    if (mysqli_connect_errno())
    {
        echo "1: Connection Failed";
        exit();
    }

    $username = $_POST["username"];
    $password = $_POST["password"];

    $namecheckquery = "SELECT username, salt, hash, highscore FROM players WHERE username = '" . $username . "';";
    
    $namecheck = mysqli_query($con, $namecheckquery) or die("2: Username check failed");
    
    if (mysqli_num_rows($namecheck) != 1)
    {
        echo "5: No user with this name OR several exist";
        exit();
    }
    
    $info = mysqli_fetch_assoc($namecheck);
    $salt = $info["salt"];
    $hash = $info["hash"];
    
    $loginhash = crypt($password, $salt);
    if ($hash != $loginhash) 
    {
        echo "6: Incorrect password";
        exit();
    }
    
    echo "0\t" . $info["highscore"];
    
?>