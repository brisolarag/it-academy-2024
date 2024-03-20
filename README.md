Como rodar a aplicação:
1. Iniciar o Docker
2. Extrair arquivo .zip e acessar o terminal na pasta raiz (dentro da pasta GabrielBrisolara_aplicacao)
3. executar:
docker compose up
ou
docker compose up -d (para rodar em modo dettach, e liberar o terminal)
Requisitos para rodar a aplicação:

- Docker
	Requisitos para o Docker no Windows:
	- WSL version 1.1.3.0 or posterior.
	- Windows 11 64-bit: Home ou Pro version 21H2 ou posterior, our Enterprise ou Education version 21H2 ou posterior.
	No Windows 10:
	Versoes recomendadas: 
		Home ou Pro22H2 (build 19045) ou posterior,
		Enterprise ou Education 22H2 (build 19045) ou posterior,
	Versões mínimas:
		Home ou Pro21H2 (build 19044) ou posterior,
		Enterprise ou Education 21H2 (build 19044) ou posteior

	- WSL 2 habilitado no Windows (Subsistema do Linux)

	Configuração de hardware mínima para o WSL2 no Windows 10 ou Windows 11:
		processador 64-bit com Second Level Adress Translation (SLAT),
		4GB RAM,
		Ativar virtualização de hardware. (ativar em Windows features).

	Requisitos para o Docker no Mac:
	Processadores Intel: 4GB RAM
	Processadores com Apple Silicon: 4GB RAM e Rosseta2 (softwareupdate --install-rosetta)

	Suporte para Linux: Ubuntu, Debian, Fedora
