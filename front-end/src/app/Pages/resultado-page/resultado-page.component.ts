import { Component, OnInit } from '@angular/core';
import { StatusService } from '../../Services/status.service';
import { SorteioService } from '../../Services/Requests/sorteio.service';
import { jsPDF } from 'jspdf';

@Component({
  selector: 'app-resultado-page',
  standalone: true,
  imports: [],
  templateUrl: './resultado-page.component.html',
  styleUrl: './resultado-page.component.scss'
})
export class ResultadoPageComponent {
  sorteioAtivo:any
  ganhadoresRodada: any
  numerosGanhadores: any[] = [];
  lenNumerosGanhadores: any;
  numerosMaisEscolhidos: any

  numerosGanhadoresString: string = "";


  constructor(private status: StatusService, private service: SorteioService) {}
  
  ngOnInit() {
    this.service.getSorteioAtivo().subscribe({
      next: (res) => {
        if (res) {
          this.sorteioAtivo = res;
        } else {
          this.sorteioAtivo = undefined;
        }
      },
      error: (error) => {
        this.sorteioAtivo = undefined;
      }
    });
    this.service.getRodadaGanhadores().subscribe({
      next: (res) => {
        if (res) {
          this.ganhadoresRodada = res;
        } else {
          this.ganhadoresRodada = undefined;
        }
      },
      error: (error) => {
        // console.error('Error fetching data:', error);
        this.ganhadoresRodada = undefined;
      }
    });
    this.service.getNumerosGanhadores().subscribe({
      next: (res) => {
        if (res && res.numerosGanhadores) {
          this.numerosGanhadores = res.numerosGanhadores;
          this.lenNumerosGanhadores = res.numerosGanhadores.length;
          this.numerosGanhadoresString = `Foram gerados os 5 números iniciais + ${5 - this.lenNumerosGanhadores} números extras.`;
        } else {
          this.numerosGanhadores = [];
        }
      },
      error: (error) => {
        // console.error('Error fetching data:', error);
        this.numerosGanhadores = [];
      }
    });
    this.service.getNumerosMaisEscolhidos().subscribe({
      next: (res) => {
        if (res) {
          this.numerosMaisEscolhidos = res;
        } else {
          this.numerosMaisEscolhidos = undefined;
        }
      },
      error: (error) => {
        // console.error('Error fetching data:', error);
        this.numerosMaisEscolhidos = undefined;
      }
    });
    
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

  printPDF(n:string, numeros:any) {
    let documento = new jsPDF('p', 'pt', 'a4');
    documento.setFont("Courier");
    documento.setFontSize(20);
    
    
    const imgData = 'assets/pdf/template.png';
    documento.addImage(imgData, 'PNG', 0, 0, 595, 842); 

    
    documento.setFontSize(30);
    documento.setTextColor(0, 0, 0);
    documento.text(n, 297, 415, {align: "center"}); 

    
    documento.setFontSize(12);
    documento.text("Números Escolhidos:", 297, 455, {align: "center"});
    documento.text(numeros.join(', '), 297, 470, {align: "center"});

    documento.save(`ganhadorSorteio_${n}.pdf`);

  }



}
