# !/usr/bin/python
import xml.etree.ElementTree


def output_squires(relationship_node_squires):
    str_return = ""
    if relationship_node_squires is not None:
        str_return = "<squires>"
        for node in relationship_node_squires:
            if node.tag == "knight":
                str_return = str_return + output_knight(node)
            elif node.tag == "squire":
                str_return = str_return + output_squire(node)
        str_return = str_return + "</squires>"
    return str_return


def output_squire(relationship_node_squire):
    str_return = ""
    if relationship_node_squire is not None:
        str_return = "<squire>"
        for node in relationship_node_squire:
            if node.tag == "name":
                str_return = str_return + "<name>" + node.text + "</name>"
        str_return = str_return + "</squire>"
    return str_return


def output_knight(relationship_node_knight):
    str_return = ""
    if relationship_node_knight is not None:
        str_return = "<knight>"
        for node in relationship_node_knight:
            if node.tag == "society_chiv_number":
                str_return = str_return + "<society_chiv_number>" + node.text + "</society_chiv_number>"
            elif node.tag == "name":
                str_return = str_return + "<name>" + node.text + "</name>"
            elif node.tag == "squires":
                str_return = str_return + output_squires(node)
        str_return = str_return + "</knight>"
    return str_return


def output_knights(node_knights):
    str_return: str = ""
    for node in node_knights:
        if node.tag == "knight":
            str_return = output_knight(node)
    return str_return


def main():
    str_output = "<knights>"
    root = tree.getroot()
    if root.tag == "knights":
        for node in root.findall("knight"):
            print("node.tag: " + node.tag)
            print("node.society_chiv_number: " + node.find('society_chiv_number').text)
            print("node.name: " + node.find('name').text)
            squires = node.get('squires')
            if squires is not None:
                print("node.squires: " + squires)
            str_output = str_output + output_knight(node) + "</knights>"
    with open("c:/temp/str_output.txt", "w") as text_file:
        text_file.write(str_output)
    output_tree = xml.etree.ElementTree.fromstring(str_output)
    output_tree.write("c:/temp/relations.xml")


if __name__ == '__main__':
    tree = xml.etree.ElementTree.parse("../ks_relationships.xml")
    main()
