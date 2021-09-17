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

  afterEach(async () => {
    const logs = await browser.manage().logs().get(logging.Type.BROWSER);
    expect(logs).not.toContain(jasmine.objectContaining(
      {level: logging.Level.SEVERE} as logging.Entry));
  });
})