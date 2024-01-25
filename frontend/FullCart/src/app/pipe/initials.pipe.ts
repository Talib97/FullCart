import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'initials'
})
export class InitialsPipe implements PipeTransform {

  transform(value:string): string {
    return value.replace(/[^a-zA-Z- ]/g, "").match(/\b\w/g)!.join('');
  }

}
