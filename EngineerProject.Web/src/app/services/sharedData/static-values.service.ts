import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StaticValuesService {

  public passwordRegex = new RegExp('^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[.!@#$%^&*])(?=.{8,})');
  public emailRegex = new RegExp('^([a-zA-Z0-9\.]+)\@([a-zA-Z0-9\.]+)\.([a-z]+)$');

  public differentPasswordError = 'Podane hasła nie pokrywają się';
  public passwordRequirementsError = 'Hasło musi posiadać: ' +
                                     '<br />&nbsp;- wielką i małą literę, ' +
                                     '<br />&nbsp;- liczbę oraz znak specjalny, ' +
                                     '<br />&nbsp;- co najmniej z 8 znaków';

  constructor() { }
}
