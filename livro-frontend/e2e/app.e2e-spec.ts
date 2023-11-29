import { OpenWebPage } from './app.po';

describe('open-web App', function() {
  let page: OpenWebPage;

  beforeEach(() => {
    page = new OpenWebPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
