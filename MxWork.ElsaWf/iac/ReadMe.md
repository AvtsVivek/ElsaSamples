1.	The files here are for provisioning a mongo db. 
	The mongo db is required for the project MxWork.ElsaWf.UserRegis.BlazorServerMongo
2.	In the DockerSwarm folder, we have a vagrant file which will bring up two vms. They will be connected as a part of docker swarm.

3.	Change directory into \iac\DockerSwarm. Then run vagrant up
	Once the machines are up and running, ensure they are provisioned properly. 

4.	Set docker host environment variable.
	set DOCKER_HOST=192.168.99.201
	Note here 192.168.99.201 is the ip address of the master node.
	Change it appropriately as required.
	Then check the command docker info

5.	Next Create the docker swarm
	Run the command docker node ls
	docker swarm init --advertise-addr=192.168.99.201. Note the ip address here.
	Next connect other nodes using a command similar to the following. The following is a sample command and not the exact command.
	docker swarm join --token SWMTKN-1-5t99rlxpxso8arcaon5qskie97faip3nv5sh97p7i5zuqo1w66-7d82zsk6she3l7p0bob4nqyl2 192.168.99.201:2377

6. 	Now deploy the vizualizer container. This will help in visualizing the containers in the swarm.
	docker stack deploy -c viz.yml viz.
	Verify with docker service ls
	Then browse to http://192.168.99.201:8085/. Note here agina the ip address.
	
7.	Now deploy mongo and mail hog
	docker stack deploy -c mongo.yml mongo
	and
	docker stack deploy -c mailhog.yml mailhog

8.	The mongo db should be ready. To verify launch(download and install if required) MondoDbCompass.
	The connection string should look something like this.
	mongodb://root:example@192.168.99.201:27017/?authSource=admin&readPreference=primary&appname=MongoDB%20Compass&ssl=false

	
	