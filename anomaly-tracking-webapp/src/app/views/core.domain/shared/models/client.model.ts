export class client {
  constructor(
    public Label: string,
    public ClientCode: string,
    public Email: string,
    public PhoneNumber: string,
    public ClientAdress: string,
    public Models: Array<any>,
    public LastModificationDate: string
  ) {}
}
