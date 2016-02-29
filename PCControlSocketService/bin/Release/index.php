<?php
error_reporting(E_ALL);
ini_set('display_erros',true);

$username 	= isset($_REQUEST['u']) ? $_REQUEST['u'] : null;
$password 	= isset($_REQUEST['p']) ? $_REQUEST['p'] : null;
$oldpassword 	= isset($_REQUEST['op']) ? $_REQUEST['op'] : null;
$act	  	= isset($_REQUEST['act']) ? $_REQUEST['act'] : null;
$msg	  	= isset($_REQUEST['msg']) ? $_REQUEST['msg'] : null;

switch($act){
	case 'lock':
		$cmd = "rundll32.exe user32.dll,LockWorkStation";
		break;
	case 'add':
		$cmd = "NET USER $username $password /ADD";
		break;
	case 'msg':
		$cmd = "mshta javascript:alert('$msg');close();";
		break;
	case 'msg2':
		$cmd = "warning.bat";
		break;  
	case 'updt':
		try {    
    		$user = new COM("WinNT://./$username,user");
		$user->ChangePassword($oldpassword, $password);
		}
		catch(com_exception $ex) {
		    $output = $ex;		 
		}
		//$cmd = "NET USER $username $password";
		break;
	default:
		$cmd = 'echo NO ACTION';
		break;
		
}

if(isset($cmd)) {
	echo $cmd;
	exec($cmd, $output);
	print_r($output);
}
	
?>