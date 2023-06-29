import { Component, OnInit } from '@angular/core';
import { TreeService } from '../services/tree.service';
import { EnterprieseTree } from '../model/EnterprieseTree';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { NestedTreeControl } from '@angular/cdk/tree';

@Component({
  selector: 'app-tree-structure',
  templateUrl: './tree-structure.component.html',
  styleUrls: ['./tree-structure.component.css']
})
export class TreeStructureComponent implements OnInit{

  nestedDataSource = new MatTreeNestedDataSource<EnterprieseTree>();
  nestedTreeControl = new NestedTreeControl<EnterprieseTree>(node => node.Children);

  constructor(private treeStruct : TreeService) { }

  ngOnInit(): void {
    this.treeStruct.GetTreeFromDB().subscribe({
      next: (tree: EnterprieseTree[]) => {
        if (tree != null) {
          this.nestedDataSource.data = tree;
          console.log(tree)
        }
      },
      error(err) {
        alert("Tree is null");
      }
    });
  };

  hasChildren(index: number, node: EnterprieseTree): boolean {
    return node?.Children?.length > 0;
  }
}
