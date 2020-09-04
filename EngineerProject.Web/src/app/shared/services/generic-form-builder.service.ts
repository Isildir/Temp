import { Injectable } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { GenericField } from '../interfaces/GenericField';

@Injectable({
  providedIn: 'root'
})
export class GenericFormBuilderService {
  public createForm(fields: GenericField[]) {
    const form: any = {};

    fields.forEach(value => {
      form[value.name] = new FormControl(value.value || '');

      if (value.isRequired || false) {
        (form[value.name] as FormControl).setValidators(Validators.required);
      }
    });

    return new FormGroup(form);
  }

  public setValue(form: FormGroup, fieldName: string, value: any) {
    form.controls[fieldName].setValue(value);
  }

  public getValue(form: FormGroup, fieldName: string) {
    return form.controls[fieldName].value;
  }

  public getValues(form: FormGroup, fieldNames: string[]) {
    const response: any = {};

    fieldNames.forEach(value => response[value] = form.controls[value].value);

    return response;
  }
}
