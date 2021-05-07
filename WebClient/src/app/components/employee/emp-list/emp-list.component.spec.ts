import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { EmployeeListComponent } from './emp-list.component';
import { SharedService } from '../../../services/shared/shared.service';
import { Employee } from '../employee.component';

describe('EmployeeListComponent', () => {
  let component: EmployeeListComponent;
  let fixture: ComponentFixture<EmployeeListComponent>;
  let service: SharedService;
  let mockList: Employee[];
  let mock: Employee;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EmployeeListComponent],
      imports: [HttpClientModule, FormsModule],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeListComponent);
    component = fixture.componentInstance;
    service = fixture.debugElement.injector.get<SharedService>(SharedService as any);
    mockList = [];
    mock = <Employee>{};
    fixture.detectChanges();
  });

  it('should create employee list component', () => {
    expect(component).toBeTruthy();
  });

  it('should call shared service when update employee list', () => {
    const spy = spyOn(service, 'getEmployeeListFromDB').and.returnValue(of(mockList));
    component.updateEmployeeList();
    expect(spy.calls.any()).toBeTruthy();
  });

  it('should set employee list value when update employee list', () => {
    spyOn(service, 'getEmployeeListFromDB').and.returnValue(of(mockList));
    component.updateEmployeeList();
    expect(component.employeeList).toEqual(mockList);
  });

  it('should call confirm window when show confirm', () => {
    const spy = spyOn(window, 'confirm');
    component.showConfirmDeleteEmployee(mock);
    expect(spy).toHaveBeenCalled();
  });
});