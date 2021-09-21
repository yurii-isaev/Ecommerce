import { DepartmentListPage } from './dep-list.po';
import { browser, logging } from 'protractor';

describe('DepartmentListPage', () => {
  let page: DepartmentListPage;

  beforeEach(() =>
    page = new DepartmentListPage());

  it('should display add department button on page', async () => {
    await page.navigateTo();
    expect(await page.isAddDepButtonDisplayed()).toBeTruthy('Add button is display');
  });

  it('should display add department button name as `Add Department` on page', async () => {
    await page.navigateTo();
    expect(await page.getAddDepartmentButtonName()).toEqual('Add Department');
  });

  it('should display modal window on page when click on add department button', async () => {
    await page.navigateTo();
    await page.getAddDepButton().click();
    await browser.sleep(1000);
    expect(await page.isModalDisplayed()).toBeTruthy('Modal window is open');
  });

  it('should display modal title as `Add Department` on page when click on add department button', async () => {
    await page.navigateTo();
    await page.getAddDepButton().click();
    await browser.sleep(1000);
    expect(await page.getModalTitle()).toEqual('Add Department');
  });

  it('should display close modal button when click on add department button', async () => {
    await page.navigateTo();
    await page.getAddDepButton().click();
    await browser.sleep(1000);
    expect(await page.isCloseModalButtonDisplayed()).toBeTruthy('Close button is display');
  });

  it('should close modal window when click on close button', async () => {
    await page.navigateTo();
    await page.getAddDepButton().click();
    await browser.sleep(1000);
    await page.getCloseModalButton().click();
    await browser.sleep(1000);
    expect(await page.isModalDisplayed()).toBeFalsy('Modal window is close');
  });

  it('should display modal component when click on add department button', async () => {
    await page.navigateTo();
    await page.getAddDepButton().click();
    await browser.sleep(1000);
    expect(await page.isModalComponentDisplayed()).toBeTruthy('Modal component is display');
  });

  it('should display departments table on page', async () => {
    await page.navigateTo();
    expect(await page.isDepartmentsTableDisplayed()).toBeTruthy('Departments table is display');
  });

  afterEach(async () => {
    const logs = await browser.manage().logs().get(logging.Type.BROWSER);
    expect(logs).not.toContain(jasmine.objectContaining(
      {level: logging.Level.SEVERE} as logging.Entry));
  });
})