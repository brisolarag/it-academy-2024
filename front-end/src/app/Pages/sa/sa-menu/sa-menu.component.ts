import { Component } from '@angular/core';
import { StatusService } from '../../../Services/status.service';
import { CommonModule, DatePipe } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { SorteioService } from '../../../Services/Requests/sorteio.service';


@Component({
  selector: 'app-sa-menu',
  standalone: true,
  imports: [CommonModule, RouterOutlet],
  templateUrl: './sa-menu.component.html',
  styleUrl: './sa-menu.component.scss'
})
export class SaMenuComponent {
  showing:string = "samenu";
  goVerSorteiosAnteriores() {
    this.showing = "sorteios_anteriores";
  }
  goSaMenu() {
    this.showing = "samenu";
  }


  numerosSorteadosRodada: any[] = [];


  apostasRodada: any;
  ganhadores: any[] = [] ;
  sorteiosAnteriores:any = [];
  
  sorteioAtivo: any;
  currentStatus: any;


  constructor(private status: StatusService, private service:SorteioService) {
  }

  
  novoSorteio() {
    this.service.postNovoSorteio()
    .subscribe(res => {
      this.sorteioAtivo = res
    })
    this.restartPage()
  }
  
  setToSorteio() {
    this.status.setSorteioToSorteio()
    .subscribe(res => {
      this.sorteioAtivo = res
    })
    this.restartPage()
  }
  setToApuracao() {
    this.service.postAdicionarGanhadores()
    .subscribe(res => {
      this.ganhadores = res;
    })
    this.status.setSorteioToApuracao()
    .subscribe(res => {
      this.sorteioAtivo = res
    })
    this.restartPage()
  }
  setToPremiacao() {
    this.status.setSorteioToPremiacao()
    .subscribe(res => {
      this.sorteioAtivo = res
    })
    this.restartPage()
  }
  setToEncerrado() {
    this.status.setSorteioToEncerrar()
    .subscribe(res => {
      this.sorteioAtivo = res
    })
    this.restartPage()
  }

  restartPage() {
    setTimeout(() => {
      window.location.reload();
    }, 750);
  }



  
  
  
  
  ngOnInit() {
    this.service.getTodasApostasAtivasAdmin()
    .subscribe(res => {
      this.apostasRodada = res;
    })
    this.service.getNumerosGanhadores().subscribe({
      next: (res) => {
        if (res && res.numerosGanhadores) {
          this.numerosSorteadosRodada = res.numerosGanhadores;
        } else {
          this.numerosSorteadosRodada = [];
        }
      },
      error: (error) => {
        this.numerosSorteadosRodada = [];
      }
    });
    this.service.getTodosGanhadores()
    .subscribe(res => {
      this.sorteiosAnteriores = res;
    })
    this.status.getSorteioAtivo().subscribe({
      next: (res) => {
        if (res) {
          this.sorteioAtivo = res;
          this.currentStatus = res.status;
        } else {
          this.sorteioAtivo = undefined;
          this.currentStatus = undefined;
        }
      },
      error: (error) => {
        this.sorteioAtivo = undefined;
        this.currentStatus = undefined;
      }
    });
  }
  
  


  transformCPF(cpf:string) {
    if (cpf.length == 11){
      return `${cpf[0] + cpf[1] + cpf[2]}.${cpf[3] + cpf[4] + cpf[5]}.${cpf[6] + cpf[7] + cpf[8]}-${cpf[9] + cpf[10]}`
    }
    return cpf;
  }
  formatarData(data: string): string {
    const dataObj = new Date(data);
    const dia = dataObj.getDate().toString().padStart(2, '0'); 
    const mes = (dataObj.getMonth() + 1).toString().padStart(2, '0'); 
    const ano = dataObj.getFullYear().toString();
    const hora = dataObj.getHours().toString().padStart(2, '0');
    const minuto = dataObj.getMinutes().toString().padStart(2, '0');
    const segundo = dataObj.getSeconds().toString().padStart(2, '0');
    return `${dia}/${mes}/${ano} | ${hora}:${minuto}:${segundo}`;
  }


}
