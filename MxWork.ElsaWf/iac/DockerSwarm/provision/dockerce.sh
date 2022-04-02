# The below line is called a shebang and specifies what shell to use to execute the rest of the file. In this case, we’re using bash.
#!/usr/bin/env bash

# sodo is not needed because Vagrant will run the script as root, so there is no need to actually use sudo.

# We are announcing to the user
echo "Installing Docker and setting it up..."

apt-get update #>/dev/null 2>&1

apt-get remove docker
apt-get remove docker-engine 
apt-get remove docker-ce 
apt-get remove docker-ce-cli 
apt-get remove docker.io

apt-get update #>/dev/null 2>&1
apt-get install -y apt-transport-https ca-certificates curl gnupg-agent software-properties-common
#
curl -fsSL https://download.docker.com/linux/ubuntu/gpg | apt-key add -
#
apt-key fingerprint 0EBFCD88
#
sudo add-apt-repository "deb [arch=amd64] https://download.docker.com/linux/ubuntu $(lsb_release -cs) stable"
#
apt-get update
#
apt-get install -y containerd.io
apt-get install -y docker-ce-cli
apt-get install -y docker-ce
#
dpkg-query -l | grep container
#
groupadd docker
#
docker --version

sudo usermod -aG docker $USER

docker info


#apt-get update >/dev/null 2>&1
#apt-get install -y apache2 >/dev/null 2>&1
#rm -rf /var/www
#ln -fs /vagrant /var/www

#cd /vagrant
#pwd
#python -m SimpleHTTPServer 80 >/dev/null 2>&1
