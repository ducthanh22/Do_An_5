import { Component } from '@angular/core';
import { ConfirmationService, MessageService, TreeNode } from 'primeng/api';
import { Module, Type, moduleTypeMap } from 'src/app/model/Enum/enum';
import { ClaimDto, CreateRoleDto, RoleDto } from 'src/app/model/role';
import { AccountService } from 'src/app/service/account.service';


@Component({
  selector: 'app-roleclaim',
  templateUrl: './roleclaim.component.html',
  styleUrls: ['./roleclaim.component.css']
})
export class RoleclaimComponent {
  selectedNodes: TreeNode[] = [];
  nodes: any[] = [];

  dataRole: CreateRoleDto = {
    role: { id: "", name: '', activeFlag: null, created: null, createdBy: null, modified: null, modifiedBy: null },
    roleClaims: []
  };
  loading: boolean = false;
  visible: boolean = false;
  name!: string;
  dataGetRole: RoleDto[] = [];
  Titile: string = '';
  detailClaim!: CreateRoleDto;
  checksave: boolean = false;
  checkbtn: boolean = false;
  getIdRole:string='';



  constructor(private accountService: AccountService, private messageService: MessageService,private confirmationService:ConfirmationService) {
  }
  ngOnInit() {
    this.nodes = this.getTreeNodes();
    console.log(this.nodes)
    this.getRole();

  }
  getRole() {
    this.accountService.GetAllRoles().subscribe(data => {
      this.dataGetRole = data
    })
  }
  getTreeNodes() {
    return Object.keys(Module)
      .filter(key => !isNaN(Number(Module[key as any])))
      .map(key => {
        const module = Module[key as any] as unknown as Module;
        const types = moduleTypeMap[module];
        return {
          label: key,
          data: module,
          children: types.map(type => ({
            label: Type[type],
            data: type
          }))
        };
      });
  }
  detail(id: string, name: string) {
    this.visible = true;
    this.Titile = 'Xem chi tiết'
    this.checksave = false;
    this.name = name;
    this.accountService.getClaimByIdRole(id).subscribe(data => {
      this.detailClaim = data;
      this.selectedNodes = [];
      this.detailClaim.roleClaims.forEach(item => {
        const node = this.nodes.find(node => node.data === Number(item.type));
        if (node) {
          const childNode = node.children.find((child: any) => child.data === Number(item.value));
          if (childNode) {
            this.selectedNodes.push(childNode); // Thêm nút con vào mảng selectedNodes
          }
        }
      });
    });
  }

  add() {
    this.visible = true;
    this.Titile = 'Thêm quyền';
    this.checkbtn=true;
    this.checksave = true;
  }

  
clickBTN(){
  this.checkbtn == true ? this.save() :this.upDate()
}

  save() {
    if (this.dataRole) {
      this.dataRole.role.name = this.name
      this.selectedNodes.forEach((x: any) => {
        if (x.children && x.children.length > 0) {
          x.children.forEach((item: any) => {
            const claim: ClaimDto = {
              type: String(x.data),
              value: String(item.data)
            };
            this.dataRole?.roleClaims.push(claim);
          });
        }
        if (x.parent != undefined) {
          const claim: ClaimDto = {
            type: String(x.parent.data),
            value: String(x.data)
          };
          this.dataRole?.roleClaims.push(claim);
        }
      })
      this.accountService.CreateRole(this.dataRole).subscribe({
        next: (res) => {
          if (res) {
            this.getRole();
            this.close();
            this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Thêm thành công' })


          }
        }

      })
    }

  }
  edit(id: string, name: string) {
    this.visible = true;
    this.Titile = 'Sửa quyền'
    this.checksave = true;
    this.checkbtn=false;
    this.name = name;
    this.accountService.getClaimByIdRole(id).subscribe(data => {
      this.detailClaim = data;
      this.getIdRole=this.detailClaim.role.id;
      this.selectedNodes = [];
      this.detailClaim.roleClaims.forEach(item => {
        const node = this.nodes.find(node => node.data === Number(item.type));
        if (node) {
          const childNode = node.children.find((child: any) => child.data === Number(item.value));
          if (childNode) {
            this.selectedNodes.push(childNode); // Thêm nút con vào mảng selectedNodes
          }
        }
      });
    });
  }

  upDate() {
    if (this.dataRole) {
      this.dataRole.role.name = this.name;
      this.dataRole.role.id = this.getIdRole;
      this.selectedNodes.forEach((x: any) => {
        if (x.children && x.children.length > 0) {
          x.children.forEach((item: any) => {
            const claim: ClaimDto = {
              type: String(x.data),
              value: String(item.data)
            };
            if (!this.isClaimExist(claim)) {
              this.dataRole?.roleClaims.push(claim);
            }
          });
        }
        if (x.parent != undefined) {
          const claim: ClaimDto = {
            type: String(x.parent.data),
            value: String(x.data)
          };
          // Kiểm tra xem claim đã tồn tại trong mảng roleClaims chưa
          if (!this.isClaimExist(claim)) {
            this.dataRole?.roleClaims.push(claim);
          }
        }
      });
      this.accountService.UpdateRole(this.dataRole).subscribe({
        next: (res) => {
          if (res) {
            this.getRole();
            this.close();
            this.messageService.add({ severity: 'warn', summary: 'Waning', detail: 'Sửa thành công' })
          }
        }
      });
    }
  }
  
  // Hàm kiểm tra xem claim đã tồn tại trong mảng roleClaims chưa
  isClaimExist(claim: ClaimDto): boolean {
    return this.dataRole?.roleClaims.some(existingClaim => existingClaim.type === claim.type && existingClaim.value === claim.value);
  }
  

  Delete(event: Event, data: string) {
    this.confirmationService.confirm({
      target: event.target as EventTarget,
      message: `Bạn có muốn xóa mã ${data}?`,
      icon: 'pi pi-info-circle',
      acceptButtonStyleClass: 'p-button-danger p-button-sm',
      accept: () => {
        if (data) {
          this.accountService.DeleteRole(data).subscribe({
            next: res => {
              if (res) {
                this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Xóa thành công' })
                this.getRole();

              }
            }
          })
        }
      },
      reject: () => {
        this.messageService.add({ severity: 'error', summary: 'Rejected', detail: 'You have rejected', life: 3000 });
      }
    });
  }

  close() {
    this.visible = false;
    this.name = '';
    this.getIdRole='';
    this.selectedNodes = [];
    this.dataRole={
      role: { id: '', name: '', activeFlag: null, created: null, createdBy: null, modified: null, modifiedBy: null },
      roleClaims: [],
    };
  }
}
