import { Component, Inject } from '@angular/core';
import { EnterprieseTree } from '../model/EnterprieseTree';
import { TreeService } from '../services/tree.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { TreeStructureComponent } from '../tree-structure/tree-structure.component';

@Component({
  selector: 'app-dialog',
  templateUrl: './dialog.component.html',
  styleUrls: ['./dialog.component.css'],
})
export class DialogComponent {

  numbers : number[] = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]

  newEnterpriese: EnterprieseTree = {
    Code: "",
    Balance: 0,
    Selected: false,
    Children : []
  };

  constructor(
    private treeService: TreeService,
    public dialogRef: MatDialogRef<DialogComponent>,
    @Inject(MAT_DIALOG_DATA) public fatherId: number
  ) { }

  addNewEnterprise() {
    if (this.newEnterpriese.Code != "") {
      for (let i = 0; i > this.numbers.length; i++) {

      if (!this.newEnterpriese.Code.startsWith(this.numbers[i].toString())) 
        this.newEnterpriese.Code = this.newEnterpriese.Code.trim().toUpperCase();
      else
        alert("Code must begin with a letter");
        
    }

      this.treeService.AddChild(this.newEnterpriese, this.fatherId).subscribe({
        next: (tree: EnterprieseTree[]) => {
          console.log(tree);
          if (tree != null)
            this.dialogRef.close(tree);
        },
        error : () => {
          alert("tree is null")
        }
      });
      
    } else {
      alert("Insert code");
    }
    
  }
}

