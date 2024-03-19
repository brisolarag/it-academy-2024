import { Component } from '@angular/core';
import { SorteioService } from '../../Services/Requests/sorteio.service';
import {NgForm} from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { StatusService } from '../../Services/status.service';
import { validateCPF } from './cpf.validator';


@Component({
  selector: 'app-apostar-page',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './apostar-page.component.html',
  styleUrl: './apostar-page.component.scss'
})
export class ApostarPageComponent {
  formData: any = {
    nome: '',
    cpf: '',
    numerosEscolhidos: [null, null, null, null, null]
  }
  onSubmit(f: NgForm) {
    console.log(f.value);
  }

nomeVerificatorString:string = "";
cpfVerificatorString:string = "";
numVerificatorString:string = "";

nomeOk: boolean = false;
CpfOk: boolean = false;
numOk: boolean = false;
todosCamposOk:boolean = false;

name: any;
sorteioAtivo: any;

  constructor(private apiService: SorteioService, private status: StatusService) {}


  submitForm(f: NgForm) {
    const numerosEscolhidosInt = this.formData.numerosEscolhidos.filter((num: string) => num !== null).map((num: string) => parseInt(num));
    this.formData.numerosEscolhidos = numerosEscolhidosInt

    this.apiService.postNovaAposta(this.formData).subscribe({
      next: response => {
        console.log('Resposta:', response);
      },
      error: error => {
        console.error('Erro:', error);
      }
    });
  }

  ngOnInit() {
    this.nomeOk = false;
    this.CpfOk = false;
    this.numOk = false;
    this.status.getSorteioAtivo().subscribe(
      res => {
        this.sorteioAtivo = res;
      },
      error => {
        console.error('Ocorreu um erro ao obter o sorteio ativo:', error);
        this.sorteioAtivo = undefined;
      }
    );
  }

  checkTodosCampos() {
    if (this.nomeOk && this.CpfOk && this.numOk) {
      return true;
    } else {
      return false;
    }
  }

  validarNome(nome: string) {
  if (!nome) {
    this.nomeVerificatorString = 'Nome precisa ser preenchido';
  } else {
    this.nomeVerificatorString = '';
    this.nomeOk = true;

  }
}

  validarCPF(cpf: string) {
    if (cpf) {
      if (validateCPF(cpf)) {
        this.cpfVerificatorString ='';
        this.CpfOk = true;
      } else {
        this.cpfVerificatorString ='CPF inválido';
      }
    } else {
      this.cpfVerificatorString = 'CPF precisa ser preenchido';
    }
  }

  validarNumero(num: string, i:number) {
    if (num == null || num == '' || num == undefined) {
      this.numVerificatorString = '';
      this.numOk = true;
    } else {
      if (isNaN(parseInt(num))) 
      {
        this.numVerificatorString = 'Número inválido';
        this.numOk = false;
      } 
      else 
      {
        const nInt = parseInt(num)
        if (nInt > 50 || nInt < 1) {
          this.numVerificatorString = 'Número precisa estar entre 1 e 50'
          this.numOk = false;
        } 
        else {
          const numMinusIgnorar = this.removerElementoPorIndice(this.formData.numerosEscolhidos, i);
          if (numMinusIgnorar.includes(num)) {
            this.numVerificatorString = 'Esse número já foi escolhido';
            this.numOk = false;
          } else {
            this.numVerificatorString = '';
            this.numOk = true;
          }
        }
      }
    }
    this.checkTodosCampos()
  }


  removerElementoPorIndice(arr: string[], indice: number): string[] {
    if (indice < 0 || indice >= arr.length) {
        throw new Error('Índice fora dos limites do array');
    }
    const newArr = arr.map(item => item);
    newArr[indice] = '';

    return newArr;
}



}



  

