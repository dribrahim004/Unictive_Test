
<?php
    $i=1;
    for ($i; $i <= 30; $i++)
    {
        $angkaempat = fmod($i, 4);
        $angkaempatbelas = fmod($i, 14);
        if ($angkaempat == 0){
            
            if (($angkaempat == 0) && ($angkaempatbelas == 0)){
                echo "Unictive Media <br>";
            }else{
                echo "Unictive <br>";
            }
            
        }else{
            echo $i ."<br>";
        }

    }
?>