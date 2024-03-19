import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';

interface Aposta {
  id: number;
  nome: string;
  cpf: string;
  data: string;
}

interface ApostaAdmin {
  id: number;
  nome: string;
  cpf: string;
  numerosEscolhidos: any[];
  data: string;
}

interface NumerosGanhadores {
  numerosGanhadores: any[];
}

interface NovaAposta {
  id: number;
  nome: string;
  cpf: string;
}

interface GanhadoresAnteriores {
  idAposta: number;
  idSorteio: number;
  nomeGanhador: string;
  cpfGanhador: string;
  numerosEscolhidos: any[];
  data: string;
}

interface GanhadoresRodada {
  id: number;
  nome: string;
  numerosEscolhidos: any[];
  data: string;
}

interface SorteioAtivo {
  id: number;
  numerosGanhadores: any[];
  data: string;
  todasApostas: any[]
  status: number;
}

interface NumerosMaisEscolhidos {
  numero: number;
  quantidade: number;
}

@Injectable({
  providedIn: 'root'
})
export class SorteioService {
  private url = "http://localhost:4652"
  sorteioAtivo: any;

  constructor(private httpClient: HttpClient) { }



  postNovaAposta(dados: any): Observable<NovaAposta> {
    return this.httpClient.post<NovaAposta>(`${this.url}/aposta`, dados)
  }

  postNovoSorteio(): Observable<any> {
    return this.httpClient.post<any>(`${this.url}/status/apostas`, null);
  }
  
  getSorteioAtivo(): Observable<SorteioAtivo> {
    return this.httpClient.get<SorteioAtivo>(this.url + "/sorteio")
  }
  
  
  
  getTodasApostasAtivas(): Observable<Aposta[]> { 
    return this.httpClient.get<Aposta[]>(this.url + "/sorteio/apostas-ativas"); 
  }
  
  getTodasApostasAtivasAdmin(): Observable<ApostaAdmin[]> { 
    return this.httpClient.get<ApostaAdmin[]>(this.url + "/sorteio/admin/apostas-ativas"); 
  }
  getNumerosGanhadores(): Observable<NumerosGanhadores> {
    return this.httpClient.get<NumerosGanhadores>(this.url + '/sorteio/admin/numeros-ganhadores');
  }
  getTodosGanhadores(): Observable<GanhadoresAnteriores[]> {
    return this.httpClient.get<GanhadoresAnteriores[]>(this.url + '/ganhadores/admin');
  }
  getRodadaGanhadores(): Observable<GanhadoresRodada[]> {
    return this.httpClient.get<GanhadoresRodada[]>(this.url + '/ganhadores/rodada');
  }
  postAdicionarGanhadores(): Observable<any> {
    return this.httpClient.post<any>(`${this.url}/ganhadores/adicionar/admin`, null);
  }
  
  getNumerosMaisEscolhidos(): Observable<NumerosMaisEscolhidos[]> {
    return this.httpClient.get<NumerosMaisEscolhidos[]>(`${this.url}/sorteio/admin/numeros-mais-escolhidos-rodada`)
  }



}
