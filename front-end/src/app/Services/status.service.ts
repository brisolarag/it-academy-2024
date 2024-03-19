import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

interface SorteioAtivo {
  id: number;
  numerosGanhadores: any[];
  data: string;
  todasApostas: any[]
  status: number;
}

@Injectable({
  providedIn: 'root'
})
export class StatusService {
  private url = "http://localhost:4652"

  constructor(private httpClient: HttpClient) {}
  

  getSorteioAtivo(): Observable<SorteioAtivo> {
    return this.httpClient.get<SorteioAtivo>(this.url + "/sorteio")
  }
  setSorteioToSorteio(): Observable<SorteioAtivo> {
    return this.httpClient.put<SorteioAtivo>(this.url + "/status/sorteio", null)
  }
  setSorteioToApuracao(): Observable<SorteioAtivo> {
    return this.httpClient.put<SorteioAtivo>(this.url + "/status/apuracao", null)
  }
  setSorteioToPremiacao(): Observable<SorteioAtivo> {
    return this.httpClient.put<SorteioAtivo>(this.url + "/status/premiacao", null)
  }
  setSorteioToEncerrar(): Observable<SorteioAtivo> {
    return this.httpClient.put<SorteioAtivo>(this.url + "/status/encerrar", null)
  }



  getStatus(n:number) {
    switch (n) {
      case 0:
        return "APOSTAS"
      case 1:
        return "SORTEIO"
      case 2:
        return "APURAÇÃO"
      case 3:
        return "PREMIAÇÃO"
      case null:
        return "Nenhum sorteio acontecendo"
      default:
        return "Nenhum sorteio acontecendo"
    }
  }
}