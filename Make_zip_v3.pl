# build Zipfile
# AGPL licensed, see AGPL 3.0 at https://www.gnu.org/licenses/agpl-3.0.en.html

BEGIN {
	open STDERR, ">&STDOUT";
}

use strict;
use warnings;
use IO::All;

use 5.010;

use File::Copy;
use File::Path;
use File::Basename;
use File::Find;
use File::Copy::Recursive qw(dircopy);

my $contents = io->file('C:/Opensim/Outworldz_Dreamgrid/Installer_Src/Setup DreamWorld/GlobalSettings.vb')->slurp;
$contents =~ s/\n//;
$contents =~ /_MyVersion As String = "(.*?)"/;
my $v = $1;
if (! $v)
{
	say ('no version!');
	exit;
}

my $Version = ` git rev-parse --short HEAD `;
chomp $Version;
$Version > io('GitVersion'); 
say ("GitVersion $Version");

my $type  = '-V' . $v; 
use Cwd;
my $dir = getcwd;


say ("Building DreamGrid$type.zip");



say ('Server Publish ? <p = publish, c = clean, enter = make the zip only>');
my $publish = <stdin>;
chomp $publish;

$v > io("$dir/Version.txt");

my $start = GetDate() . " " . GetTime() . "\n";
my @languages = qw (ar ar-SA es-ES fa ca cs da de el en es-MX eu fa-IR fi fr ga he  is it nl-NL no pl pt pt-BR ru sv tr vi zh-cn zh-tw zh-Hans-HK    );
foreach my $lang (@languages)
{
	JustDelete ($lang);
}

my @a = io->dir('.')->all;

foreach my $f (@a)
{	
	if ($f->name =~ /tmp.*\.html/) {	
		$f->unlink;
	}
}

say("Clean up opensim");
my @deletions = (
	"$dir/OutworldzFiles/AutoBackup",	
	"$dir/OutworldzFiles/Opensim/WifiPages-Custom",	
	"$dir/OutworldzFiles/Opensim/bin/WifiPages-Custom",
	"$dir/OutworldzFiles/Opensim/WifiPages",	
	"$dir/OutworldzFiles/Opensim/bin/WifiPages",
	"$dir/OutworldzFiles/Opensim/bin/datasnapshot",
	"$dir/OutworldzFiles/Opensim/bin/assetcache",
	"$dir/OutworldzFiles/Opensim/bin/j2kDecodeCache",
	"$dir/OutworldzFiles/Opensim/bin/MeshCache",
	"$dir/OutworldzFiles/Opensim/bin/ScriptEngines",
	"$dir/OutworldzFiles/Opensim/bin/maptiles",
	"$dir/OutworldzFiles/Opensim/bin/Regions",
	"$dir/OutworldzFiles/Opensim/bin/bakes",	
	"$dir/OutworldzFiles/Opensim/bin/fsassets",	
	"$dir/OutworldzFiles/Apache/logs/",    
);

foreach my $path ( @deletions) {
	say ($path);
	DeleteandKeep($path);
}

DelZips();
DelMaps();

unlink "$dir/BareTail.udm";

unlink "$dir/SET_externalIP-log.txt";
unlink "$dir/OutworldzFiles/Photo.png";
unlink "$dir/OutworldzFiles/XYSettings.ini";
unlink "$dir/OutworldzFiles/Opensim/bin/OpensimConsoleHistory.txt" ;
unlink "$dir/OutworldzFiles/Opensim/bin/LocalUserStatistics.db" ;
unlink "$dir/OutworldzFiles/BanList.txt" ;

#Setting
unlink "$dir/Outworldzfiles/Settings.ini" ;

#logs
unlink "$dir/Outworldzfiles/Icecast/log/error.log" ;
unlink "$dir/Outworldzfiles/Icecast/log/access.log" ;

unlink "$dir/UpdateGrid.log";
unlink "$dir/OutworldzFiles/Apache/htdocp
s/Search/flog.log" ;
unlink "$dir/OutworldzFiles/Opensim/bin/Robust.log" ;
unlink "$dir/OutworldzFiles/Opensim/bin/RobustStats.log" ;
unlink "$dir/OutworldzFiles/Opensim/bin/Opensimstats.log" ;
###

unlink "$dir/OutworldzFiles/Logs/Restart.log" ;
unlink "$dir/OutworldzFiles/Logs/Diagnostics.log" ;
unlink "$dir/OutworldzFiles/Logs/Outworldz.log" ;
unlink "$dir/OutworldzFiles/Logs/upnp.log" ;
unlink "$dir/OutworldzFiles/Logs/http.log" ;
unlink "$dir/OutworldzFiles/Logs/Error.log" ;
unlink "$dir/OutworldzFiles/Logs/Teleport.log" ;

#zips
unlink "../Zips/DreamGrid$type.zip" ;
unlink "../Zips/Outworldz-Update$type.zip" ;
unlink "$dir/DreamGrid.zip";

say "DLL List Build";
use File::Find;

open (OUT, ">", 'dlls.txt');

find({ wanted => \&process_file, no_chdir => 1 }, $dir . '/OutworldzFiles/Opensim/bin/');

# these are needed in this file even if deleted on disk.
print OUT "\\OutworldzFiles\\opensim\\bin\\jOpensimProfile.Modules.dll\n";
print OUT "\\OutworldzFiles\\opensim\\bin\\jOpensimSearch.Modules.dll\n";
print OUT "\\OutworldzFiles\\opensim\\bin\\jOpensimMoney.Modules.dll\n";

close OUT;

unlink "$dir/Start.exe.lastcodeanalysissucceeded";
unlink "$dir/Start.exe.CodeAnalysisLog.xml";

if ($publish =~ /c|p/ ) {
	say("Mysql");
	chdir(qq!$dir/OutworldzFiles/mysql/bin/!);
	print `mysqladmin.exe --port 3306 -u root shutdown`;
	sleep(2);
	chdir ($dir);
	DeleteandKeep("$dir/OutworldzFiles/mysql/data");
	say ("Cleaned");	
}

#say('Copy Manuals');
#if (!dircopy ($dir . '/OutworldzFiles/Help/', "Y:/Inetpub/Secondlife/Outworldz_Installer/Help"))  {die $!;}
        
    
say("Signing Release");    
my $exes = "$dir/Installer_Src/Setup DreamWorld/bin/Release";
sign($exes);

use File::Copy::Recursive qw(dircopy);
dircopy($exes,$dir) or die("$!\n");

   
say("Signing copies");
use IO::All;
sign($dir);	

print "Processing Main Zip\n";

JustDelete('\\Opensim\\Zip');

my @files =   `cmd /c dir /b `;

# Just do files, dirs are explicitly copied over
foreach my $file (@files) {
    chomp $file;
    next if -d "$dir/$file";
    
    next if $file =~ /^\./;
    ProcessFile ("\"$dir\\$file\"" );
}
say("Adding folders");
# just dirs
ProcessDir ('MSFT_Runtimes');
ProcessDir ('Read.Me');
ProcessDir ('Licenses_to_Content');
ProcessDir ('OutworldzFiles');


foreach my $lang (@languages)
{
    ProcessDir ($lang);
}

say("Drop mysql files from update");
# now delete the mysql from the UPDATE

say("Drop Mysql from update");
DeleteandKeep('\\Opensim\\Zip\\Outworldzfiles\\mysql\\Data');
say("Drop Jopensim from update");
DeleteandKeep('\\Opensim\\Zip\\Outworldzfiles\\Apache\\htdocs\\jOpensim');
if (!copy ('\\Opensim\\Zip\\Outworldzfiles\\jOpensim_Files\\default.htm', '\\Opensim\\Zip\\Outworldzfiles\\Apache\\htdocs\\jOpensim\\default.htm'))  {die $!;}

say("Drop Opensim Source code from update");
JustDelete('/Opensim/Zip/Outworldzfiles/Opensim/Opensim');
JustDelete('/Opensim/Zip/Outworldzfiles/Opensim/packages');
JustDelete('/Opensim/Zip/Outworldzfiles/Opensim/packages');
JustDelete('/Opensim/Zip/Outworldzfiles/Opensim/packages');
JustDelete('/Opensim/Zip/Outworldzfiles/Opensim/runprebuild19.sh');


JustDelete('/Opensim/Zip/Outworldzfiles/Opensim/clean.sh');
JustDelete('/Opensim/Zip/Outworldzfiles/Opensim/cleanaot.sh');
JustDelete('/Opensim/Zip/Outworldzfiles/Opensim/prebuild48.xml');
JustDelete('/Opensim/Zip/Outworldzfiles/Opensim/makeaot.sh');
JustDelete('/Opensim/Zip/Outworldzfiles/Opensim/runprebuild19.bat');


JustDelete('/Opensim/Zip/Outworldzfiles/Opensim/addon-modules');
JustDelete('/Opensim/Zip/Outworldzfiles/Opensim/.vs');
JustDelete('/Opensim/Zip/Outworldzfiles/Opensim/.nant');
JustDelete('/Opensim/Zip/Outworldzfiles/Opensim/bin/addin-db-002');
JustDelete('/Opensim/Zip/Outworldzfiles/Opensim/Doc');
JustDelete('/Opensim/Zip/Outworldzfiles/Opensim/Prebuild');
JustDelete('/Opensim/Zip/Outworldzfiles/Opensim/share');
JustDelete('/Opensim/Zip/Outworldzfiles/Opensim/Thirdparty');
JustDelete('/Opensim/Zip/Outworldzfiles/Opensim/.git');
JustDelete('/Opensim/Zip/Outworldzfiles/Opensim/.gitignore');
JustDelete('/Opensim/Zip/Outworldzfiles/Opensim/.hgignore');
JustDelete('/Opensim/Zip/Outworldzfiles/Opensim/BUILDING.md');
JustDelete('/Opensim/Zip/Outworldzfiles/Opensim/compile.bat');	
JustDelete('/Opensim/Zip/Outworldzfiles/Opensim/makefile');
JustDelete('/Opensim/Zip/Outworldzfiles/Opensim/nant-color');
JustDelete('/Opensim/Zip/Outworldzfiles/Opensim/BUILDING.md');	
JustDelete('/Opensim/Zip/Outworldzfiles/Opensim/Opensim.sln');
JustDelete('/Opensim/Zip/Outworldzfiles/Opensim/Opensim.build');
JustDelete('/Opensim/Zip/Outworldzfiles/Opensim/prebuild.xml');
JustDelete('/Opensim/Zip/Outworldzfiles/Opensim/runprebuild.bat');	
JustDelete('/Opensim/Zip/Outworldzfiles/Opensim/runprebuild.sh');
JustDelete('/Opensim/Zip/Outworldzfiles/Opensim/TESTING.txt');
JustDelete('/Opensim/Zip/Make_zip_v3.pl');
JustDelete('/Opensim/Zip/Start.vshost.exe.manifest');
JustDelete('/Opensim/Zip/Start.vshost.exe.config');
JustDelete('/Opensim/Zip/Start.vshost.exe');
JustDelete('/Opensim/Zip/OutworldzFiles/Opensim/bin/.git');

print "Make zip\n";
unlink "/Opensim/Zips/DreamGrid$type.zip";
my $src ='/Opensim/zip/';
my $dest = "/Opensim/Zips/DreamGrid$type.zip";

use Archive::Zip qw( :ERROR_CODES :CONSTANTS );

my $zip = Archive::Zip->new();
$zip->addTree($src);

unless  ( $zip->writeToFileNamed($dest) == AZ_OK ) {
	die 'write error';
}

unlink "Y:/Inetpub/Secondlife/Outworldz_Installer/Grid/Older Versions/DreamGrid/DreamGrid-Update$type.zip" ;
unlink "Y:/Inetpub/Secondlife/Outworldz_Installer/Grid/Older Versions/DreamGrid/DreamGrid$type.zip" ;	
	
if (!copy ("../Zips/DreamGrid$type.zip", "Y:/Inetpub/Secondlife/Outworldz_Installer/Grid/Other Versions/DreamGrid$type.zip"))  {die $!;}
if (!copy ("../Zips/DreamGrid$type.zip", "Y:/Inetpub/Secondlife/Outworldz_Installer/Grid/Other Versions/DreamGrid-Update$type.zip"))  {die $!;}
	
	
if ($publish =~ /p/ ) {

	sleep(1);

	
	if (!copy ('Read.Me/Revisions.txt', 'Y:/Inetpub/Secondlife/Outworldz_Installer/Grid/Revisions.txt'))  {die $!;}	
	if (!copy ('Read.me/Revisions.txt', 'Y:/Inetpub/Secondlife/Outworldz_Installer/Revisions.txt'))  {die $!;}
	
	say ("Unlinking");	
	unlink "Y:/Inetpub/Secondlife/Outworldz_Installer/Grid/DreamGrid.zip" ;
	unlink "Y:/Inetpub/Secondlife/Outworldz_Installer/Grid/DreamGrid-Update.zip" ;	

	say ("Publishing now");
	
	if (!copy ("../Zips/DreamGrid$type.zip", "Y:/Inetpub/Secondlife/Outworldz_Installer/Grid/DreamGrid.zip"))  {die $!;}
	if (!copy ("../Zips/DreamGrid$type.zip", "Y:/Inetpub/Secondlife/Outworldz_Installer/Grid/DreamGrid-Update.zip"))  {die $!;}
	if (!copy ("../Zips/DreamGrid$type.zip", "G:/Dropbox/Dreamworld/Zip/DreamGrid.zip"))  {die $!;}
		
	print "Revisions\n";
	
	if (!copy ('outworldzfiles\\Help\\Dreamgrid Manual.pdf', 'Y:/Inetpub/Secondlife/Outworldz_Installer/Grid/Dreamgrid Manual.pdf'))  {die $!;}
	$v > io("Y:/Inetpub/Secondlife/Outworldz_Installer/Grid/Version.txt");
	
}
foreach my $lang (@languages)
{
	JustDelete ($lang);
}

print $start . "\n";
print GetDate() . " " . GetTime() . "\n";
say "Done!";


sub Write
{
	my $dest = shift;
	my $content = shift;
	open OUT, ">$dest" || die $!;
	print OUT $content;
	close OUT;
}

sub ProcessFile
{
	my $file = shift;
	
	my $x = `xcopy $file ..\\Zip\\`;
	$x =~ s/\n//g;
	if ($x =~ /File\(s\) copied/) {
		print "$file ok\n";
	} else {
		print "$file Fail: $x\n";
		exit;
	}
	
	
}

sub ProcessDir
{
	my $file = shift;
	return if $file =~ /\.rtf$/;
	
	my $x = `xcopy /E /I /C \\Opensim\\Outworldz_Dreamgrid\\$file  \\Opensim\\zip\\$file`;
	my $y = $x;
	$x =~ s/\n//g;
	if ($x =~ /(\d+) File\(s\) copied/) {
		print "$y\n";
		if ($1 == 0) 
		{
			say ($file . " failed to copy\n $y");
			die;
		}
	} else {
		print "$file Fail: $y\n";
		exit;
	}
	
	
}

sub rm {
	
my $path = shift;
	
	my $errors;
	while ($_ = glob("'$path/*'")) {
		rmtree($_)
		  or ++$errors, warn("Can't remove $_: $!");
	}
	
	#exit(1) if $errors;
}

sub JustDelete {
	
	my $path = shift;
	if (! -d $path) {
		unlink $path;
	}
		
	use File::Path;	
	rmtree $path;	
}

sub DeleteandKeep {

	my $path = shift;	
	use File::Path;	
	rmtree $path;	 
	while (-e $path) 
	{
		rmtree $path;	 
		print "Directory '$path' still exists\n";
		sleep(1);
	}
    
	mkdir $path ;
	open (FILE, '>', $path . '/.keep') or die;
	print FILE "git will not save empty folders unless there is a file in it. This is a placeholder only\n";
	close FILE;
	
}


# for importers
sub Perlunzip {
	
	use Archive::Zip qw(:ERROR_CODES :CONSTANTS);
	use Exporter 'import';
	
	my ($zip_file , $out_file, $filter) = @_;
	$zip_file = $dir . '/Outworldzfiles/'. $zip_file;
	$out_file = $dir . '/Outworldzfiles/'. $out_file;
	
	my $zip = Archive::Zip->new($zip_file);
	unless ($zip->extractTree($filter || '', $out_file) == AZ_OK) {
		warn "unzip not successful: $!\n";
	}
}


sub DelZips
{
	while ($_ = glob("$dir/*.zip")) {
		unlink ($_)  or die("Can't remove $_: $!");
	}	
}


sub DelMaps
{
	while ($_ = glob("$dir/Outworldzfiles/opensim/bin/Map-*.png")) {
		unlink ($_)  or die("Can't remove $_: $!");
	}	
}


sub sign
{
		
	my @files = io->dir(shift)->all(0);  

	my @signs;
	foreach my $file (@files) {
		my $name = $file->name;
		next if $name =~ /Microsoft|Debug|\.git|baretail|Downloader|Bouncy|Google|Tuple|packages/;
		
		if ($name =~ /Start\.exe/i)
		{
			my $bp =1 ;			
		}
		
		if ($name =~ /dll$|exe$/ ) {
			
			my $r = qq!../Certs/sigcheck64.exe "$name"!;
			print $r. "\n";
			my $result1 = `$r`;
			
			print $result1;
			
			if ($result1 !~ /Unsigned/) {
				next;
			}
			$result1 =~ s/\n//g;
			if ($result1 =~ /Verified(.*)/i) {
				push(@signs,$name);
			};
			
			
			my $f = qq!../Certs/digicertutil.exe sign /noInput /sha1 "d7ea8e5f8e6d27b138ecd93811daa6b02b0ba333" "$name"!;
			print $f . "\n";
			my $result = `$f`;
			print $result. "\n";
			$result =~ s/\n/|/g;
			
			
			if ($result !~ /success/) {
				say ("***** Failed to sign!");
				die;
			}
		}
	}
	
	say (join("\n",@signs));
}
sub process_file {
   # if (-f $_) {
       # print "This is a file: $_\n";
    #} else {
       # print "This is not file: $_\n";
   # }
	if ($_ !~ /dll$/) {
		return;
	}
	my $fullpath = $_;
	$fullpath =~ s/$dir//g;
	$fullpath =~ s/\//\\/g;
	
	print OUT $fullpath . "\n";	
}

sub GetDate
{
	use DateTime;
	return DateTime->now->set_time_zone('America/Chicago')->ymd('/');
}

sub GetTime
{
	use DateTime;
	return DateTime->now->set_time_zone('America/Chicago')->hms(':');
}
